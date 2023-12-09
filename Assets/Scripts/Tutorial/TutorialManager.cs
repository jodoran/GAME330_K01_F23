using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour, IDSTapListener
{
    public static TutorialManager instance;
    public GameObject gm;
    public AudioClip clip;

    public bool tutorial = true;
    public int index;
    public float nextTalkDuration;

    private bool IsPressed;

    [Header("==========Assign==========")]
    public GameObject upScreen01;
    public GameObject downScreen01;
    public GameObject upScreen02;
    public GameObject downScreen02;
    public GameObject upScreen03;
    public GameObject upScreen04;
    public GameObject costBar;
    public GameObject tutorialStore;
    public GameObject fakeStore;
    public GameObject RealStore;
    public GameObject BackBtn;
    public GameObject TempNextBtn;
    public GameObject NextBtn;
    public BtnManager store;
    public GameManager gameManager;
    public GameObject mainCam;
    public GameObject tutoCam;
    public RectTransform CostBar;


    //public GameObject btn;

    private Animator upScreen02anim;
    private Animator downScreen02anim;

    private Coroutine sizeDeltaChangeCoroutine;
    private const float sizeDeltaChangeDuration = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Time.timeScale = 1.0f;
        upScreen01.SetActive(true);
        downScreen01.SetActive(true);
    }

    public void Store()
    {
        downScreen02.SetActive(false);
        fakeStore.SetActive(false);
        RealStore.SetActive(true);

        upScreen03.SetActive(true);
        gm.SetActive(false);
        BtnDisActive();
        Next();
    }

    public void BtnActive()
    {
        if (upScreen03.activeSelf || upScreen04.activeSelf)
        {
            TempNextBtn.SetActive(true);
        }
        else
        {
            NextBtn.SetActive(true);
        }
        BackBtn.SetActive(true);
    }

    public void BtnDisActive()
    {
        if (upScreen03.activeSelf || upScreen04.activeSelf)
        {
            TempNextBtn.SetActive(false);
        }

        NextBtn.SetActive(false);
        BackBtn.SetActive(false);
    }

    public void Next()
    {
        //print("1");

        if (index == 0)
        {
            //print("0");
            //btn.SetActive(false);
            upScreen01.SetActive(false);
            downScreen01.SetActive(false);

        }
        else if (index == 1)
        {
            gm.SetActive(false);
            upScreen02.SetActive(true);
            downScreen02.SetActive(true);
            upScreen02anim = upScreen02.GetComponent<Animator>();
            downScreen02anim = downScreen02.GetComponent<Animator>();
            StartCoroutine(Index01());
        }
        else if (index == 2)
        {
            upScreen02.SetActive(false);
            StopCoroutine(Index01());
        }
        else if (index == 3)
        {
            if (IsPressed)
                return;
            IsPressed = true;
            upScreen04.SetActive(true);
            upScreen02anim = upScreen04.GetComponent<Animator>();
            gm.SetActive(true);
            mainCam.SetActive(true);
            tutoCam.SetActive(false);
            //gameManager.GetComponent<GameManager>().enabled = true;
            //gameManager.GetComponent<TutorialGameManager>().enabled = false;
            tutorial = false;
            store.type = BtnType.Store;
            store.OnBtnClick();
            gameManager.generateTime = 2;
            gameManager.generateText.text = "Max " + gameManager.generateTime + "s";
            UnitController castle = gameManager.unitPrefab[4].GetComponent<UnitController>();
            castle.unitCost = 8;
            gameManager.castlecostText.text = "" + castle.unitCost;
            gameManager.purchase();
            StartCoroutine(IncreaseCostCoroutine());
        }
        else
        {
            SceneManager.LoadScene("Title");
            Time.timeScale = 1.0f;
        }

        index++;
    }

    public IEnumerator IncreaseCostCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameManager.generateTime);

            // Increase the cost by 1 and ensure it doesn't exceed the maximum
            gameManager.cost = Mathf.Min(gameManager.cost + 1, 100);
            UpdateCost();
            //Debug.Log("Current Cost: " + cost);
        }
    }

    public void UpdateCost()
    {
        // Update the TextMeshPro text to display the current cost
        gameManager.costText.text = "" + gameManager.cost;
        gameManager.costTextShdow.text = "" + gameManager.cost;
        //CostBar.sizeDelta = new Vector2((50 * cost), CostBar.sizeDelta.y);

        // Stop any ongoing sizeDeltaChangeCoroutine
        if (sizeDeltaChangeCoroutine != null)
        {
            StopCoroutine(sizeDeltaChangeCoroutine);
        }

        // Start a new coroutine to smoothly change the x value of CostBar.sizeDelta
        sizeDeltaChangeCoroutine = StartCoroutine(SmoothSizeDeltaChange(new Vector2((50 * gameManager.cost), CostBar.sizeDelta.y), sizeDeltaChangeDuration));
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

    IEnumerator Index01()
    {
        yield return new WaitForSeconds(18f);
        costBar.SetActive(true);
        upScreen02anim.SetTrigger("02");
        downScreen02anim.SetTrigger("02");
        nextTalkDuration = 8f;
        yield return new WaitForSeconds(8);
        nextTalkDuration = 2.5f;
        downScreen02anim.SetTrigger("tutorialStore");
        yield return new WaitForSeconds(2.5f);
        nextTalkDuration = 3f;
        yield return new WaitForSeconds(5);
        nextTalkDuration = 2.5f;
        StopCoroutine(Index01());
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {
        if (IsPressed)
        {
            return;
        }
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if (!IsPressed)
        {
            if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
            {
                print("3");
                IsPressed = true;
                StartCoroutine(SimulateButtonClick());
            }
        }
    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
        if (IsPressed)
        {
            IsPressed = false;
        }
    }

    IEnumerator SimulateButtonClick()
    {
        print("4");
        float buttonPressTime = 0.1f;
        yield return new WaitForSeconds(buttonPressTime);
        if (!IsPressed)
        {
            print("5");
            GetComponent<EventTrigger>().OnPointerDown(null);
        }
    }
}
