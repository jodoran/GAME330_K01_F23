using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour, IDSTapListener
{
    public static TutorialManager instance;
    public AudioClip clip;

    public bool tutorial = true;
    public int index;

    private bool IsPressed;

    [Header("==========Assign==========")]
    public GameObject upScreen01;
    public GameObject downScreen01;
    //public GameObject btn;

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
    }

    public void Next()
    {
        print("1");
        if (index == 0)
        {
            print("0");
            //btn.SetActive(false);
            upScreen01.SetActive(false);
            downScreen01.SetActive(false);
        }
        else if (index == 1)
        {
            SceneManager.LoadScene("Title");
        }

        index++;
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
