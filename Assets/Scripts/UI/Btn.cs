using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Btn : MonoBehaviour, IDSTapListener
{
    Animator anim;

    public MYType.Unit Type;
    public GameManager gm;

    bool IsPressed = false;

    public void Start()
    {
        gm = GameObject.FindFirstObjectByType<GameManager>();
        anim = GetComponent<Animator>();
    }

    public void OnBtnClick()
    {
        switch (Type)
        {
            case MYType.Unit.Sword:
                Debug.Log("Sword");
                gm.Index = 0;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Guard:
                Debug.Log("Guard");
                gm.Index = 1;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Range:
                Debug.Log("Range");
                gm.Index = 2;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Wizard:
                Debug.Log("Wizard");
                gm.Index = 3;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Castle:
                //Debug.Log("Castle");
                gm.Index = 4;
                gm.SpawnUnit();
                break;
        }
    }

    public void NotEnoughCost()
    {
        anim.SetTrigger("NotEnoughCost");
    }

    public void Purchase()
    {
        anim.SetTrigger("Purchase");
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