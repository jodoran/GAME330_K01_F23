using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    
    public class EnemySpawner : MonoBehaviour
    {
        //lis of gameobject
        //public List<GameObject> enemies;

        //[SerializeField] private EnemySO enemyData;  
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnRate = 5f;
        [SerializeField] private float spawnInterval = 5f;

        // ���� �ð�
        private float timer;

        // ���̺� ���� ī��Ʈ
        private int wave;

        // ���ʹ��� ���� �迭
        EnemySO[] EnemySOArray;

        // ���̺� ���� �ð�
        public int waveTransferTime = 3;

        // �� ���̺� �� ����ð�
        public int waveTime = 10;

        // �� ���� ���͹� �ð� 
        public int enemyIntervalTime = 1;

        private float enemyTimer = 0f;

        bool isEnd = false;

        private void Start()
        {
            // ���ʹ��� ���� �迭�� �� ���� �ֱ�
            EnemySOArray = new EnemySO[]{
                UnitManager.Instance.GetEnemySO(EnemyType.BigGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.SmallGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.ShootingGuy)
            };
            timer = 0;
        }

        private void Update()
        {
            if(isEnd)
                return;
            WaveChanger();
           
        }

        // �� ���� �Լ�
        void SpawnEnemy(int wave)
        {
            // ������ ��ġ ����
            var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[spawnPositionIndex].position;

            // Rotation ����
            Quaternion rotation = Quaternion.Euler(0, 180, 0);

            int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(wave));
            EnemySO spawnEnemy = EnemySOArray[enemytypeIndex];
            Debug.Log("�� ���� : " + spawnEnemy.enemyType + " ��ġ : " + spawnPositionIndex);

            Instantiate(spawnEnemy.enemyPrefab, spawnPoint, rotation);
        }
       
        void WaveChanger()
        {
            // ���� �ð� �߰�
            timer += Time.deltaTime;
            enemyTimer += Time.deltaTime;

            // ���� ���̺� (���� �ð��� ���̺� �ð��� ���� ��, 0~9 �� 0����, 10~19�� 1����
            int currentWave = (int)timer / waveTime;
            if (wave != currentWave)
            {
                if (currentWave > 2)
                {
                    Debug.Log("END GAME");
                    isEnd = true;
                    return;
                }

                Debug.Log("change wave : " + currentWave);
                wave = currentWave;
                enemyTimer = 0;
            }

            // ���½ð����� ����
            bool isSleep = (int)timer % waveTime < waveTransferTime;

            // ���� �ð��̸� �ƹ��͵� ����
            if (isSleep)
                return;

            // ���͹� �ð����� ũ�� �ٽ� 0���� �����ϰ� ������ �����մϴ�.
            if (enemyTimer >= this.enemyIntervalTime)
                enemyTimer = 0;

            // �� ����
            if (enemyTimer < Time.deltaTime)
                SpawnEnemy(wave);
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