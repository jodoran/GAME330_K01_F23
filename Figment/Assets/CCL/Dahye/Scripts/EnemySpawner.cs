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

        // 진행 시간
        private float timer;

        // 웨이브 진행 카운트
        private int wave;

        // 에너미의 정보 배열
        EnemySO[] EnemySOArray;

        // 웨이브 쉬는 시간
        public int waveTransferTime = 3;

        // 각 웨이브 총 진행시간
        public int waveTime = 10;

        // 적 스폰 인터벌 시간 
        public int enemyIntervalTime = 1;

        private float enemyTimer = 0f;

        bool isEnd = false;

        private void Start()
        {
            // 에너미의 정보 배열에 각 정보 넣기
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

        // 적 생성 함수
        void SpawnEnemy(int wave)
        {
            // 스폰할 위치 지정
            var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[spawnPositionIndex].position;

            // Rotation 지정
            Quaternion rotation = Quaternion.Euler(0, 180, 0);

            int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(wave));
            EnemySO spawnEnemy = EnemySOArray[enemytypeIndex];
            Debug.Log("적 스폰 : " + spawnEnemy.enemyType + " 위치 : " + spawnPositionIndex);

            Instantiate(spawnEnemy.enemyPrefab, spawnPoint, rotation);
        }
       
        void WaveChanger()
        {
            // 진행 시간 추가
            timer += Time.deltaTime;
            enemyTimer += Time.deltaTime;

            // 현재 웨이브 (현재 시간에 웨이브 시간을 나눈 몫, 0~9 면 0라운드, 10~19면 1라운드
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

            // 쉬는시간인지 여부
            bool isSleep = (int)timer % waveTime < waveTransferTime;

            // 쉬는 시간이면 아무것도 안함
            if (isSleep)
                return;

            // 인터벌 시간보다 크면 다시 0으로 생성하고 적으로 생성합니다.
            if (enemyTimer >= this.enemyIntervalTime)
                enemyTimer = 0;

            // 적 생성
            if (enemyTimer < Time.deltaTime)
                SpawnEnemy(wave);
        }

        //Wave별 적 생성 확률
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

        //스폰 확률 계산기
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

        //wave : enemy count 지정 후 모두 hp = 0 한다면 다음 웨이브 -> 새로생성
        //에너미 파괴시 setActive false
    }

}