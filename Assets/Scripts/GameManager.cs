using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform spawnpoint;
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
    private bool hasBtnUpBeenCalled = false;

    private MainCamera mainCamera;

    [Header("==========Cost==========")]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI costTextShdow;
    public TextMeshProUGUI generateText;
    public TextMeshProUGUI castlecostText;
    public RectTransform CostBar;
    public int generateTime;
    public int cost;

    private const int maxCost = 10;

    [Header("==========HP==========")]
    public UnitController curBlueCastle;
    public UnitController curRedCastle;
    public RectTransform blueHPBar;
    public RectTransform redHPBar;
    public TextMeshProUGUI blueHPText;
    public TextMeshProUGUI redHPText;

    private const int maxBlueHP = 500;
    private const int maxRedHP = 500;

    public void Start()
    {
        //#.InputSystem
        mainCamera = FindObjectOfType<MainCamera>();

        costTextAnimator = costText.GetComponent<Animator>();
        defeatAnimator = defeat.GetComponent<Animator>();
        retryAnimator = retry.GetComponent<Animator>();
        victoryAnimator = victory.GetComponent<Animator>();
        unitPrefab[4].GetComponent<UnitController>().unitCost = 5;

        StartCoroutine(IncreaseCostCoroutine());
        checkCastleCoroutine = StartCoroutine(CheckCastleHPCoroutine());
        UpdateCost();
    }

    public void Update()
    {
        UpdateBlueHPBar();
        UpdateRedHPBar();

        GameInput();
    }

    public void GameInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput == 0f && !hasBtnUpBeenCalled)
        {
            mainCamera.BtnUp();
            hasBtnUpBeenCalled = true;
        }
        else if (horizontalInput != 0f)
        {
            hasBtnUpBeenCalled = false;
        }

        switch (horizontalInput)
        {
            case 1f: // Right
                //Debug.Log("Right");
                mainCamera.Right();
                break;
            case -1f: // Left
                //Debug.Log("Left");
                mainCamera.Left();
                break;
            //case 0f: // NotPressed
            //    //Debug.Log("NotPressed");
            //    mainCamera.BtnUp();
            //    break;
        }

        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1f: // UP
                Debug.Log("Up");
                break;
            case -1f: // Down
                Debug.Log("Down");
                break;
            case 0f: // NotPressed
                break;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 button pressed");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire2 button pressed");
        }

        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("Fire3 button pressed");
        }
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

    public void UpdateBlueHPBar()
    {
        float normalizedHP = Mathf.Clamp01((float)curBlueCastle.HP / maxBlueHP);
        float newWidth = normalizedHP * maxBlueHP * 5;

        blueHPBar.sizeDelta = new Vector2(newWidth, blueHPBar.sizeDelta.y);
        blueHPText.text = "" + curBlueCastle.HP;
    }

    public void UpdateRedHPBar()
    {
        float normalizedHP = Mathf.Clamp01((float)curRedCastle.HP / maxRedHP);
        float newWidth = normalizedHP * maxRedHP * 5;

       redHPBar.sizeDelta = new Vector2(newWidth, redHPBar.sizeDelta.y);
       redHPText.text = "" + curRedCastle.HP;
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
                retry.SetActive(true);
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
                retry.SetActive(true);
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