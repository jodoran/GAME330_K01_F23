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
    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

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

        if (isPaused)
        {
            soundManager.PauseBGM(); // BGM 일시정지
        }
        else
        {
            soundManager.ResumeBGM(); // BGM 재개
        }

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
