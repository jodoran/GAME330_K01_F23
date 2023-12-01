using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform spawnpoint;
    public UnitController curBlueCastle;
    public UnitController curRedCastle;
    public GameObject victory;
    public GameObject defeat;
    public GameObject retry;
    public GameObject[] unitPrefab;
    public Btn[] Btn;
    public int Index;

    private Animator costTextAnimator;
    private Animator defeatAnimator;
    private Animator retryAnimator;
    private Animator victoryAnimator;
    private const float sizeDeltaChangeDuration = 1f;
    private Coroutine sizeDeltaChangeCoroutine;
    private Coroutine checkCastleCoroutine;
    private bool gameEnded = false;

    [Header("==========Cost==========")]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI costTextShdow;
    public TextMeshProUGUI generateText;
    public TextMeshProUGUI castlecostText;
    public RectTransform CostBar;
    public int generateTime;
    public int cost;

    private const int maxCost = 10;

    public void Start()
    {
        costTextAnimator = costText.GetComponent<Animator>();
        defeatAnimator = defeat.GetComponent<Animator>();
        retryAnimator = retry.GetComponent<Animator>();
        victoryAnimator = victory.GetComponent<Animator>();
        unitPrefab[4].GetComponent<UnitController>().unitCost = 5;

        StartCoroutine(IncreaseCostCoroutine());
        checkCastleCoroutine = StartCoroutine(CheckCastleHPCoroutine());
        UpdateCost();
    }

    public void SpawnUnit()
    {
        // Check if there is enough cost to spawn the unit
        if (cost >= unitPrefab[Index].GetComponent<UnitController>().unitCost)
        {
            //GameObject newUnit = Instantiate(unitPrefab[Index], spawnpoint.position, Quaternion.identity);

            // Reduce the cost by the unit's cost
            //cost -= newUnit.GetComponent<UnitController>().unitCost;

            switch (Index)
            {
                case 0:
                    GameObject newWarrior = Instantiate(unitPrefab[Index], spawnpoint.position, Quaternion.identity);
                    cost -= newWarrior.GetComponent<UnitController>().unitCost;
                    break;
                case 1:
                    GameObject newGuard = Instantiate(unitPrefab[Index], spawnpoint.position, Quaternion.identity);
                    cost -= newGuard.GetComponent<UnitController>().unitCost;
                    break;
                case 2:
                    GameObject newArcher = Instantiate(unitPrefab[Index], spawnpoint.position, Quaternion.identity);
                    cost -= newArcher.GetComponent<UnitController>().unitCost;
                    break;
                case 3:
                    GameObject newWizard = Instantiate(unitPrefab[Index], spawnpoint.position, Quaternion.identity);
                    cost -= newWizard.GetComponent<UnitController>().unitCost;
                    break;
                case 4:
                    Debug.Log("Castle");
                    if (generateTime > 2)
                    {
                        UnitController castle = unitPrefab[Index].GetComponent<UnitController>();

                        cost -= castle.unitCost;

                        castle.unitCost++;
                        castlecostText.text = "" + castle.unitCost;
                        
                        if (generateTime > 3)
                        {
                            generateTime--;
                            generateText.text = "" + generateTime + "s";
                        }
                        else
                        {
                            generateTime--;
                            generateText.text = "Max " + generateTime + "s";
                        }
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("Not enough cost to spawn this unit!");
            Btn[Index].NotEnoughCost();
            costTextAnimator.SetTrigger("NotEnoughCost");
        }

        UpdateCost();
    }

    public void UpdateCost()
    {
        // Update the TextMeshPro text to display the current cost
        costText.text = "" + cost;
        costTextShdow.text = "" + cost;
        //CostBar.sizeDelta = new Vector2((50 * cost), CostBar.sizeDelta.y);

        // Stop any ongoing sizeDeltaChangeCoroutine
        if (sizeDeltaChangeCoroutine != null)
        {
            StopCoroutine(sizeDeltaChangeCoroutine);
        }

        // Start a new coroutine to smoothly change the x value of CostBar.sizeDelta
        sizeDeltaChangeCoroutine = StartCoroutine(SmoothSizeDeltaChange(new Vector2((50 * cost), CostBar.sizeDelta.y), sizeDeltaChangeDuration));
    }

    public IEnumerator IncreaseCostCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(generateTime);

            // Increase the cost by 1 and ensure it doesn't exceed the maximum
            cost = Mathf.Min(cost + 1, maxCost);
            UpdateCost();
            //Debug.Log("Current Cost: " + cost);
        }
    }

    private IEnumerator SmoothSizeDeltaChange(Vector2 targetSizeDelta, float duration)
    {
        float elapsedTime = 0f;
        Vector2 startSizeDelta = CostBar.sizeDelta;

        while (elapsedTime < duration)
        {
            CostBar.sizeDelta = Vector2.Lerp(startSizeDelta, targetSizeDelta, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is exactly the target size
        CostBar.sizeDelta = targetSizeDelta;

        // Reset the coroutine reference
        sizeDeltaChangeCoroutine = null;
    }

    private IEnumerator CheckCastleHPCoroutine()
    {
        while (true)
        {
            yield return null;

            if (gameEnded)
                yield break;

            if (curBlueCastle.HP <= 0)
            {
                Debug.Log("Defeat");
                Time.timeScale = 0f;
                defeat.SetActive(true);
                //defeatAnimator.SetTrigger("Show");
                StopCoroutine(checkCastleCoroutine);
                gameEnded = true;
                yield break;
            }
            else if (curRedCastle.HP <= 0)
            {
                Debug.Log("Victory");
                Time.timeScale = 0f;
                victory.SetActive(true);
                //victoryAnimator.SetTrigger("Show");
                StopCoroutine(checkCastleCoroutine);
                gameEnded = true;
                yield break;
            }
        }
    }

    private void StopCastleCheckCoroutine()
    {
        if (checkCastleCoroutine != null)
        {
            StopCoroutine(checkCastleCoroutine);
            checkCastleCoroutine = null;
        }
    }

    public void wannaRetry()
    {
        if(victory.activeSelf)
        {
            victoryAnimator.SetTrigger("Hide");
            Invoke(nameof(Hide), 0.2f);
        }
        if(defeat.activeSelf)
        {
            defeatAnimator.SetTrigger("Hide");
            Invoke(nameof(Hide), 0.2f);
        }

        //Invoke(nameof(Hide), 0.2f);
    }

    public void Hide()
    {
        victory.SetActive(false);
        defeat.SetActive(false);
        retry.SetActive(true);
    }

    public void Retry()
    {
        retryAnimator.SetTrigger("Hide");
        Invoke(nameof(Reset), 1);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BacktoTitle()
    {
        retryAnimator.SetTrigger("Hide");
        Invoke(nameof(Title), 1);
    }

    public void Title()
    {
        print("BacktoTitle");
        //SceneManager.LoadScene("Title");
    }
}