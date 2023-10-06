using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnInterval;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    private int currentWaveIndex = -1;

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(2f); // Wait time before starting the next wave

        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Starting wave: " + wave.waveName);
        for (int i = 0; i < wave.enemyCount; i++)
        {
            if (spawnPoints.Length > 0)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Debug.LogError("No spawn points defined!");
            }
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        // Wait for all enemies of this wave to be defeated before starting the next wave
        // This can be replaced with a more sophisticated check
        yield return new WaitForSeconds(5f);
        StartCoroutine(StartNextWave());
    }
}
