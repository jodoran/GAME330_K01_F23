using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemySO enemySO;

    [Tooltip("스폰되는 위치 배열")]
    [SerializeField] private Transform[] spawnPoints;

    [Tooltip("스폰 주기")]
    [SerializeField] private float enemyIntervalTime = 1f;

    [Tooltip("적 정보 배열")]
    [SerializeField] private EnemySO[] enemySoArray;

    /// <summary>
    /// 임시 사운드 추후 사운드매니저 변경
    /// </summary>
    [SerializeField] private AudioClip spawnsfx;

    /// <summary>
    /// 주어진 EnemyType에 해당하는 EnemySO를 반환하는 메서드
    /// </summary>
    /// <returns> enemySO 배열에서 enemyType과 일치하는 첫 번째 EnemySO를 찾아 반환 </returns>
    private EnemySO GetEnemySO(EnemyType enemyType)
    {
        return System.Array.Find(enemySoArray, x => x.EnemyType == enemyType);
    }

    /// <summary>
    /// 적을 생성하는 코루틴을 시작합니다.
    /// </summary>
    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    /// <summary>
    /// 라운드에 맞춰 적을 생성합니다.
    /// </summary>
    /// <param name="currWave"></param>
    IEnumerator spawnEnemy()
    {
        // 게임오버 상태가 아니면서, Final Wave가 아닐 땐
        while (GameManager.Instance.IsGameActive)
        {
            // 적 스폰 기다림
            yield return new WaitForSeconds(enemyIntervalTime);
            if (GameManager.Instance.IsBreak)
                continue;

            // 현재 라운드
            var round = GameManager.Instance.CurrentRound;

            // 스폰할 위치 지정
            var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[spawnPositionIndex].position;

            // Rotation 지정
            Quaternion rotation = Quaternion.Euler(0, 180, 0);

            // 적 타입의 인덱스 계산
            int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(round));

            // 적 타입을 불러옴
            EnemySO enemySO = enemySoArray[enemytypeIndex];

            //생성된 obj 에 EnemyBase 클래스컴포넌트를 가져와서 셋팅
            var obj = Instantiate(enemySO.Prefeb, spawnPoint, rotation);
            var enemy = obj.GetComponent<Enemy_DH>();
            enemy.Setting(enemySO);

            SoundManager.Instance.PlayEffectSound(spawnsfx);

        }
    }

    //Wave별 적 생성 확률
    private float[] WaveProbabilityArray(int waveNumber)
    {
        if (waveNumber == 0)
        {
            return new float[] {
                    enemySoArray[0].wave1st,
                    enemySoArray[1].wave1st,
                    enemySoArray[2].wave1st
                };
        }
        if (waveNumber == 1)
        {
            return new float[] {
                    enemySoArray[0].wave2nd,
                    enemySoArray[1].wave2nd,
                    enemySoArray[2].wave2nd
                };
        }
        if (waveNumber == 2)
        {
            return new float[] {
                    enemySoArray[0].wave3rd,
                    enemySoArray[1].wave3rd,
                    enemySoArray[2].wave3rd
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
}
