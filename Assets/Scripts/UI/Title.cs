using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum TitleType
{
    Start,
    Tutorial,
    Quit
}

public class Title : MonoBehaviour, IDSTapListener
{
    public TitleType type;
    public GameObject Select;

    bool IsPressed = false;

    [Header("==========Audio=========")]
    public AudioClip[] clip;

    public void OnBtnClick()
    {
        switch (type)
        {
            case TitleType.Start:
                if (!IsPressed)
                    return;
                Debug.Log("Start");
                IsPressed = true;
                Time.timeScale = 1;
                SoundManager.instance.SFXPlay("Btn", clip[0]);
                Invoke(nameof(StarttheGame), 1f);
                break;
            case TitleType.Tutorial:
                if (!IsPressed)
                    return;
                Debug.Log("Tutorial");
                Time.timeScale = 1;
                SoundManager.instance.SFXPlay("Btn", clip[0]);
                Invoke(nameof(Tutorial), 1f);
                break;
            case TitleType.Quit:
                Debug.Log("Quit");
                SoundManager.instance.SFXPlay("Btn", clip[0]);
                Application.Quit();
                break;
        }
    }

    public void StarttheGame()
    {
        SceneManager.LoadScene("NintendoDSTowerDefense");
        IsPressed = false;
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        IsPressed = false;
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
        Select.SetActive(false);

        if (!IsPressed)
        {
            if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
            {
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
        float buttonPressTime = 0.1f;
        yield return new WaitForSeconds(buttonPressTime);
        if (!IsPressed)
        {
            GetComponent<EventTrigger>().OnPointerDown(null);
        }
    }
}
