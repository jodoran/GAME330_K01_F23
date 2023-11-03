using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtYourServiceButton : MonoBehaviour, IDSTapListener
{
    bool IsPressed = false;

    public Image TopScreenImage;
    public Image BottomScreenImage;

    float TargetXValue;

    // Start is called before the first frame update
    void Start()
    {
        TargetXValue = -256.0f;
    }

    // Update is called once per frame
    void Update()
    {
        const float SNAP_SPEED = 24.0f;
        Vector2 currPosition = TopScreenImage.rectTransform.anchoredPosition;
        currPosition.x += (TargetXValue - currPosition.x) * SNAP_SPEED * Time.deltaTime;
        TopScreenImage.rectTransform.anchoredPosition = currPosition;

        currPosition = BottomScreenImage.rectTransform.anchoredPosition;
        currPosition.x += (TargetXValue - currPosition.x) * SNAP_SPEED * Time.deltaTime;
        BottomScreenImage.rectTransform.anchoredPosition = currPosition;
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if(DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
        {
            IsPressed = true;

            Debug.Log("I've been clicked!");
            TargetXValue = 0.0f;
        }
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {
    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
        if(IsPressed)
        {
            IsPressed = false;

            Debug.Log("I've been released!");
            TargetXValue = -256.0f;
        }
    }
}
