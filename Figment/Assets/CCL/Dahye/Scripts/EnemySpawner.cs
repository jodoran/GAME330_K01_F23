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

        public int waveTime = 10;    // 각 웨이브 총 진행시간
        public int enemyIntervalTime = 1;// 적 스폰 인터벌 시간 
        private int savedWave;            // 웨이브 진행 카운트
        private float timer;         // 진행 시간 웨이브 변경에 사용 
       
        EnemySO[] EnemySOArray;      // 에너미의 정보 배열

        bool isSpawning = false;

        //임시 사운드 추후 사운드매니저 변경
        public AudioSource audioSource;
        public AudioClip enemysfx;

        

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            //enemysfx = GetComponent<AudioClip>();

            // 에너미의 정보 배열에 각 정보 넣기
            EnemySOArray = new EnemySO[]{
                UnitManager.Instance.GetEnemySO(EnemyType.BigGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.SmallGuy),
                UnitManager.Instance.GetEnemySO(EnemyType.ShootingGuy)
            };


            timer = 0;
            savedWave = 0;

            StartCoroutine(SpawnEnemy(savedWave));
        }
        //savedWave : 현재 저장된 웨이브 1프레임 전 웨이브 
        //updatedtWave : 새로운 웨이브 1프레임 후 웨이브
        private void Update()
        {
            if (GameManager.Instance.IsGameActive)
                return;

            // 시간 진행상황
            timer += Time.deltaTime;

            // 현재 웨이브
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

        // 적 생성 함수
        IEnumerator SpawnEnemy(int currWave)
        {
            isSpawning = true;
            while (GameManager.Instance.IsGameActive) // 게임오버 상태가 아니면서, Final Wave가 아닐 땐
            {
                yield return new WaitForSeconds(enemyIntervalTime);

                // 스폰할 위치 지정
                var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
                var spawnPoint = spawnPoints[spawnPositionIndex].position;

                // Rotation 지정
                Quaternion rotation = Quaternion.Euler(0, 180, 0);
                 
                int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(currWave));
                //EnemySO spawnEnemy = EnemySOArray[0];
                EnemySO spawnEnemy = EnemySOArray[enemytypeIndex];
                Debug.Log("적 스폰 : " + spawnEnemy.enemyType + " 위치 : " + spawnPositionIndex);

                //생성된 obj 에 EnemyBase 클래스컴포넌트를 가져와서 
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
            // 에너미가 다 죽었을 경우
            if (UnitManager.Instance.EnemyAllDieCheck() && !isSpawning)
            {
                Debug.Log("생성불가");
                //StartCoroutine(SpawnEnemy(savedWave));
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

        private void OnDestroy()
        {
            foreach (var enemy in FindObjectsOfType<EnemyBase>())
            {
                enemy.OnDead -= WaveCheck;
            }
        }


    }

}