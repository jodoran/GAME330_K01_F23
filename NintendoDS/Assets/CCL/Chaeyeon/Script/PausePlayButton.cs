using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PausePlayButton : MonoBehaviour, IDSTapListener
{
    public Button button;
    public Sprite pauseSprite;
    public Sprite playSprite;

    private bool isPaused = false;

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
        {
           
            TogglePausePlay();
        }
    }

    public void TogglePausePlay()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log(isPaused);

        button.image.sprite = isPaused ? playSprite : pauseSprite;
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {

    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
 
    }

}
