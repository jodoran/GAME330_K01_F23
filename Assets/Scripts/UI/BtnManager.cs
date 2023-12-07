using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BtnType
{
    Store,
    Cancel
}

public class BtnManager : MonoBehaviour, IDSTapListener
{
    Animator anim;
    public BtnType type;

    public GameObject Store;
    public GameObject MoveBtn;
    bool IsPressed = false;

    public void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void OnBtnClick()
    {
        switch (type)
        {
            case BtnType.Store:

                break;
            case BtnType.Cancel:

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