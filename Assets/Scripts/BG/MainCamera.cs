using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCamera : MonoBehaviour, IDSTapListener
{
    public GameManager gm;
    public int speed;
    public float minX; // min x location
    public float maxX; // max x location
    public float h;
    public bool isBtnDown;

    bool IsPressed = false;

    [Header("==========Audio=========")]
    public AudioClip clip;

    public void Update()
    {
        Move();
    }

    public void Right()
    {
        h = 1;
        isBtnDown = true;
        //print("Right");
    }

    public void Left()
    {
        h = -1;
        isBtnDown = true;
        //print("Left");
    }

    public void BtnUp()
    {
        h = 0;
        isBtnDown = false;
        //print("BtnUp");
    }

    public void Move()
    {
        if (isBtnDown)
        {
            Vector3 curPos = transform.position;

            curPos.x += h * speed * Time.deltaTime;

            curPos.x = Mathf.Clamp(curPos.x, minX, maxX);

            transform.position = curPos;
        }
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = tapPosition;

        // UI 버튼이 터치되었는지 확인
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            UnityEngine.UI.Button button = result.gameObject.GetComponent<UnityEngine.UI.Button>();
            EventTrigger eventTrigger = result.gameObject.GetComponent<EventTrigger>();

            if (button != null && eventTrigger != null && !gm.isPause)
            {
                //print(result.gameObject);
                IsPressed = true;
                eventTrigger.OnPointerDown(null);
                SoundManager.instance.SFXPlay("Btn", clip);
                //GetComponent<EventTrigger>().OnPointerDown(null);
            }
        }
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {
    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
        if (IsPressed)
        {
            IsPressed = false;
            BtnUp();
        }
    }
}
