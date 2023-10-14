using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    
    public class EnemySpawner : MonoBehaviour
    {
        //[SerializeField] private EnemySO enemyData;  
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnRate = 5f;
        [SerializeField] private float spawnInterval = 5f;

        public int waveTime = 10;    // �� ���̺� �� ����ð�
        public int enemyIntervalTime = 1;// �� ���� ���͹� �ð� 
        private int savedWave;            // ���̺� ���� ī��Ʈ
        private float timer;         // ���� �ð� ���̺� ���濡 ��� 
       
        EnemySO[] EnemySOArray;      // ���ʹ��� ���� �迭

        bool isSpawning = false;

        //�ӽ� ���� ���� ����Ŵ��� ����
        public AudioSource audioSource;
        public AudioClip enemysfx;

        

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            //enemysfx = GetComponent<AudioClip>();

            // ���ʹ��� ���� �迭�� �� ���� �ֱ�
            EnemySOArray = new EnemySO[]{
                UnitManager.Instance.GetEnemySO(EnemyType.BigGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.SmallGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.ShootingGuy)
            };


            timer = 0;
            savedWave = 0;

            StartCoroutine(SpawnEnemy(savedWave));
        }
        //savedWave : ���� ����� ���̺� 1������ �� ���̺� 
        //updatedtWave : ���ο� ���̺� 1������ �� ���̺�
        private void Update()
        {
            if (GameManager.Instance.IsGameActive)
                return;

            // �ð� �����Ȳ
            timer += Time.deltaTime;

            // ���� ���̺�
            int updatedWave = (int)timer / waveTime;
            if (savedWave != updatedWave)
            {
                savedWave = updatedWave;

                if (savedWave > 2)
                {
                    GameManager.Instance.GameOver();
                    return;
                }

                if (!isSpawning)
                {
                    StartCoroutine(SpawnEnemy(savedWave));
                }
            }
        }

        // �� ���� �Լ�
        IEnumerator SpawnEnemy(int currWave)
        {
            isSpawning = true;
            while (GameManager.Instance.IsGameActive) // ���ӿ��� ���°� �ƴϸ鼭, Final Wave�� �ƴ� ��
            {
                yield return new WaitForSeconds(enemyIntervalTime);

                // ������ ��ġ ����
                var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
                var spawnPoint = spawnPoints[spawnPositionIndex].position;

                // Rotation ����
                Quaternion rotation = Quaternion.Euler(0, 180, 0);
                 
                int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(currWave));
                //EnemySO spawnEnemy = EnemySOArray[0];
                EnemySO spawnEnemy = EnemySOArray[enemytypeIndex];
                Debug.Log("�� ���� : " + spawnEnemy.enemyType + " ��ġ : " + spawnPositionIndex);

                //������ obj �� EnemyBase Ŭ����������Ʈ�� �����ͼ� 
                GameObject obj = Instantiate(spawnEnemy.enemyPrefab, spawnPoint, rotation);
                obj.GetComponent<EnemyBase>().enemySetting(spawnEnemy);
                obj.GetComponent<EnemyBase>().OnDead = WaveCheck;


                audioSource.PlayOneShot(enemysfx);

                if (savedWave != currWave)
                    break;
            }
            isSpawning = false;
        }

        void WaveCheck()
        {
            // ���ʹ̰� �� �׾��� ���
            if (UnitManager.Instance.EnemyAllDieCheck() && !isSpawning)
            {
                Debug.Log("�����Ұ�");
                //StartCoroutine(SpawnEnemy(savedWave));
            }
        }

        //Wave�� �� ���� Ȯ��
        private float[] WaveProbabilityArray(int waveNumber)
        {
            if (waveNumber == 0)
            {
                return new float[] {
                    EnemySOArray[0].wave1st,
                    EnemySOArray[1].wave1st,
                    EnemySOArray[2].wave1st
                };
            }
            if (waveNumber == 1)
            {
                return new float[] {
                    EnemySOArray[0].wave2nd,
                    EnemySOArray[1].wave2nd,
                    EnemySOArray[2].wave2nd
                };
            }
            if (waveNumber == 2)
            {
                return new float[] {
                    EnemySOArray[0].wave3rd,
                    EnemySOArray[1].wave3rd,
                    EnemySOArray[2].wave3rd
                };
            }

            return new float[] { };
        }

        //���� Ȯ�� ����
        private int SpawnProbabilityChoose(float[] probs)
        {
            float total = 0;

            foreach (float element in probs)
            {
                total += element; //element
            }
            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                    return i;
                else
                    randomPoint -= probs[i];
            }
            return probs.Length - 1;
        }

        private void OnDestroy()
        {
            foreach (var enemy in FindObjectsOfType<EnemyBase>())
            {
                enemy.OnDead -= WaveCheck;
            }
        }


    }

}