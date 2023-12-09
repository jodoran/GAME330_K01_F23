using System.Collections;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject[] unitPrefab;

    [Header("==========Cost==========")]
    public float initialGenerateTime = 10f;
    public float timeMultiplier = 0.95f;
    public int cost;

    private const int maxCost = 10;

    private void Start()
    {
        StartCoroutine(IncreaseCostCoroutine());
    }

    private IEnumerator IncreaseCostCoroutine()
    {
        float currentGenerateTime = initialGenerateTime;

        while (true)
        {
            // Set a random spawn interval within the currentGenerateTime range
            float randomSpawnInterval = Random.Range(0.8f * currentGenerateTime, 1.2f * currentGenerateTime);
            yield return new WaitForSeconds(randomSpawnInterval);

            // Increase the cost by 1 and ensure it doesn't exceed the maximum
            cost = Mathf.Min(cost + 1, maxCost);

            // Check if there is enough cost to spawn a unit
            if (cost >= 4)
            {
                // Select a random unit index based on cost
                int randomUnitIndex = SelectRandomUnitIndex();

                // Spawn the selected unit
                GameObject newUnit = Instantiate(unitPrefab[randomUnitIndex], spawnpoint.position, Quaternion.identity);

                // Reduce the cost by the unit's cost
                cost -= newUnit.GetComponent<UnitController>().unitCost;
            }

            // Decrease currentGenerateTime using a logarithmic function
            currentGenerateTime = Mathf.Max(currentGenerateTime * timeMultiplier, 2f);
        }
    }

    private int SelectRandomUnitIndex()
    {
        // Define the cost thresholds for each unit
        int[] costThresholds = { 3, 4, 5, 7 };

        // Randomly select an index based on cost thresholds
        int randomCost = Random.Range(3, 8);
        int selectedUnitIndex = 0;

        for (int i = 0; i < costThresholds.Length; i++)
        {
            if (randomCost <= costThresholds[i])
            {
                selectedUnitIndex = i;
                break;
            }
        }

        return selectedUnitIndex;
    }
}