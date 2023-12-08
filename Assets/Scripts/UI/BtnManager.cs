using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BtnType
{
    Store,
    Cancel,
    Pause
}

public class BtnManager : MonoBehaviour, IDSTapListener
{
    public BtnType type;

    public GameObject Menu;
    public GameObject UpScreen;
    public GameObject DownScreen;
    bool IsPressed = false;

    private Animator menu;
    private Animator upScreen;
    private Animator downScreen;

    [Header("==========Audio=========")]
    public AudioClip[] clip;

    public void Awake()
    {
        Menu = GameObject.Find("Menu");
        UpScreen = GameObject.Find("UpScreen");
        DownScreen = GameObject.Find("DownScreen");
    }

    public void OnBtnClick()
    {
        menu = Menu.GetComponent<Animator>();
        upScreen = UpScreen.GetComponent<Animator>();
        downScreen = DownScreen.GetComponent<Animator>();

        switch (type)
        {
            case BtnType.Store:
                menu.SetTrigger("Open");
                SoundManager.instance.SFXPlay("Btn", clip[0]);
                break;
            case BtnType.Cancel:
                menu.SetTrigger("Close");
                SoundManager.instance.SFXPlay("Btn", clip[0]);
                break;
            case BtnType.Pause:
                Time.timeScale = 1;
                upScreen.SetTrigger("Resume");
                downScreen.SetTrigger("Resume");
                SoundManager.instance.SFXPlay("Btn", clip[0]);
                break;
        }
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if (!IsPressed)
        {
            if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
            {
                IsPressed = true;
                StartCoroutine(SimulateButtonClick());
            }
        }
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {
        if (IsPressed)
        {
            return;
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
        float buttonPressTime = 0.1f;
        yield return new WaitForSeconds(buttonPressTime);
        if (!IsPressed)
        {
            GetComponent<EventTrigger>().OnPointerDown(null);
        }
    }
}