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

        // 진행 시간
        private float timer;

        // 웨이브 진행 카운트
        private int currWave;
        private int finalWave;
        private int waveIndex;

        // 에너미의 정보 배열
        EnemySO[] EnemySOArray;

        // 각 웨이브 총 진행시간
        public int waveTime = 10;

        // 적 스폰 인터벌 시간 
        public int enemyIntervalTime = 1;

        [Header("웨이브 별 총 에너미 수량")]
        [SerializeField] private int maxSpawnCount;
        [SerializeField] private int currSpawnCount;
        private float enemyTimer = 0f;


        bool isEnd = false;

        //임시 사운드 추후 사운드매니저 변경
        public AudioSource audioSource;
        public AudioClip enemysfx;

        

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            //enemysfx = GetComponent<AudioClip>();

            timer = 0;
            currSpawnCount = 0;
            currWave = 0;

            // 에너미의 정보 배열에 각 정보 넣기
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

        // 적 생성 함수
        IEnumerator SpawnEnemy()
        {

            while(currSpawnCount < maxSpawnCount && currWave != finalWave)
            {
                yield return new WaitForSeconds(enemyIntervalTime);

                // 스폰할 위치 지정
                var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
                var spawnPoint = spawnPoints[spawnPositionIndex].position;

                // Rotation 지정
                Quaternion rotation = Quaternion.Euler(0, 180, 0);

                int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(currWave));
                EnemySO spawnEnemy = EnemySOArray[enemytypeIndex];
                Debug.Log("적 스폰 : " + spawnEnemy.enemyType + " 위치 : " + spawnPositionIndex);

                //생성된 obj 에 EnemyBase 클래스컴포넌트를 가져와서 
                GameObject obj = Instantiate(spawnEnemy.enemyPrefab, spawnPoint, rotation);
                obj.GetComponent<EnemyBase>().enemySetting(spawnEnemy);

                audioSource.PlayOneShot(enemysfx);

                currSpawnCount++;
                maxSpawnCount -= 1;
                Debug.Log("현재 생성된에너미 : " + currSpawnCount + "남은 에너미 " + maxSpawnCount);
            }
        }

        //bool EnemyIsAlive()
        //{
           
        //}


        void WaveChanger()
        {
            //시간 진행상황
            timer += Time.deltaTime;

            //웨이브 체인저
            //현재 남아있는 에너미 수량이 0일 시 + 파이널 웨이브가 아니라면 다음 웨이브로 넘어간다. 
            if (currWave != finalWave && currSpawnCount == 0)
            {
                if (finalWave > 2 && currSpawnCount == 0)
                {
                    isEnd = true;
                    GameManager.Instance.GameOver();
                    Debug.Log("게임매니저에서 가져온 게임오버!");

                    return;
                }
                Debug.Log("change wave : " + finalWave);
            }
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