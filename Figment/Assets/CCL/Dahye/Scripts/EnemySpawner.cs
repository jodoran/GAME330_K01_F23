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

        // ���� �ð�
        private float timer;

        // ���̺� ���� ī��Ʈ
        private int currWave;
        private int finalWave;
        private int waveIndex;

        // ���ʹ��� ���� �迭
        EnemySO[] EnemySOArray;

        // �� ���̺� �� ����ð�
        public int waveTime = 10;

        // �� ���� ���͹� �ð� 
        public int enemyIntervalTime = 1;

        [Header("���̺� �� �� ���ʹ� ����")]
        [SerializeField] private int maxSpawnCount;
        [SerializeField] private int currSpawnCount;
        private float enemyTimer = 0f;


        bool isEnd = false;

        //�ӽ� ���� ���� ����Ŵ��� ����
        public AudioSource audioSource;
        public AudioClip enemysfx;

        

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            //enemysfx = GetComponent<AudioClip>();

            timer = 0;
            currSpawnCount = 0;
            currWave = 0;

            // ���ʹ��� ���� �迭�� �� ���� �ֱ�
            EnemySOArray = new EnemySO[]{
                UnitManager.Instance.GetEnemySO(EnemyType.BigGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.SmallGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.ShootingGuy)
            };


            StartCoroutine(SpawnEnemy());
        }

        private void Update()
        {
            if(isEnd)
                return;
            WaveChanger();
           
        }

        // �� ���� �Լ�
        IEnumerator SpawnEnemy()
        {

            while(currSpawnCount < maxSpawnCount && currWave != finalWave)
            {
                yield return new WaitForSeconds(enemyIntervalTime);

                // ������ ��ġ ����
                var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
                var spawnPoint = spawnPoints[spawnPositionIndex].position;

                // Rotation ����
                Quaternion rotation = Quaternion.Euler(0, 180, 0);

                int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(currWave));
                EnemySO spawnEnemy = EnemySOArray[enemytypeIndex];
                Debug.Log("�� ���� : " + spawnEnemy.enemyType + " ��ġ : " + spawnPositionIndex);

                //������ obj �� EnemyBase Ŭ����������Ʈ�� �����ͼ� 
                GameObject obj = Instantiate(spawnEnemy.enemyPrefab, spawnPoint, rotation);
                obj.GetComponent<EnemyBase>().enemySetting(spawnEnemy);

                audioSource.PlayOneShot(enemysfx);

                currSpawnCount++;
                maxSpawnCount -= 1;
                Debug.Log("���� �����ȿ��ʹ� : " + currSpawnCount + "���� ���ʹ� " + maxSpawnCount);
            }
        }

        //bool EnemyIsAlive()
        //{
           
        //}


        void WaveChanger()
        {
            //�ð� �����Ȳ
            timer += Time.deltaTime;

            //���̺� ü����
            //���� �����ִ� ���ʹ� ������ 0�� �� + ���̳� ���̺갡 �ƴ϶�� ���� ���̺�� �Ѿ��. 
            if (currWave != finalWave && currSpawnCount == 0)
            {
                if (finalWave > 2 && currSpawnCount == 0)
                {
                    isEnd = true;
                    GameManager.Instance.GameOver();
                    Debug.Log("���ӸŴ������� ������ ���ӿ���!");

                    return;
                }
                Debug.Log("change wave : " + finalWave);
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

        //wave : enemy count ���� �� ��� hp = 0 �Ѵٸ� ���� ���̺� -> ���λ���
        //���ʹ� �ı��� setActive false
    }

}