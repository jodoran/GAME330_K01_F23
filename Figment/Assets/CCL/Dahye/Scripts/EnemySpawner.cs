using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemySO enemySO;

    [Tooltip("�����Ǵ� ��ġ �迭")]
    [SerializeField] private Transform[] spawnPoints;

    [Tooltip("���� �ֱ�")]
    [SerializeField] private float enemyIntervalTime = 1f;

    [Tooltip("�� ���� �迭")]
    [SerializeField] private EnemySO[] enemySoArray;

    /// <summary>
    /// �ӽ� ���� ���� ����Ŵ��� ����
    /// </summary>
    [SerializeField] private AudioClip spawnsfx;

    /// <summary>
    /// �־��� EnemyType�� �ش��ϴ� EnemySO�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <returns> enemySO �迭���� enemyType�� ��ġ�ϴ� ù ��° EnemySO�� ã�� ��ȯ </returns>
    private EnemySO GetEnemySO(EnemyType enemyType)
    {
        return System.Array.Find(enemySoArray, x => x.EnemyType == enemyType);
    }

    /// <summary>
    /// ���� �����ϴ� �ڷ�ƾ�� �����մϴ�.
    /// </summary>
    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    /// <summary>
    /// ���忡 ���� ���� �����մϴ�.
    /// </summary>
    /// <param name="currWave"></param>
    IEnumerator spawnEnemy()
    {
        // ���ӿ��� ���°� �ƴϸ鼭, Final Wave�� �ƴ� ��
        while (GameManager.Instance.IsGameActive)
        {
            // �� ���� ��ٸ�
            yield return new WaitForSeconds(enemyIntervalTime);
            if (GameManager.Instance.IsBreak)
                continue;

            // ���� ����
            var round = GameManager.Instance.CurrentRound;

            // ������ ��ġ ����
            var spawnPositionIndex = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[spawnPositionIndex].position;

            // Rotation ����
            Quaternion rotation = Quaternion.Euler(0, 180, 0);

            // �� Ÿ���� �ε��� ���
            int enemytypeIndex = SpawnProbabilityChoose(WaveProbabilityArray(round));

            // �� Ÿ���� �ҷ���
            EnemySO enemySO = enemySoArray[enemytypeIndex];

            //������ obj �� EnemyBase Ŭ����������Ʈ�� �����ͼ� ����
            var obj = Instantiate(enemySO.Prefeb, spawnPoint, rotation);
            var enemy = obj.GetComponent<Enemy_DH>();
            enemy.Setting(enemySO);

            SoundManager.Instance.PlayEffectSound(spawnsfx);

        }
    }

    //Wave�� �� ���� Ȯ��
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
}
