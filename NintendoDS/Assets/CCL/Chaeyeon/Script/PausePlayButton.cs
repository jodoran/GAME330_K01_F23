using UnityEngine;
using UnityEngine.UI; 

public class PausePlayButton : MonoBehaviour
{
    public Button button;     
    public Sprite pauseSprite; 
    public Sprite playSprite; 
    private bool isPaused = false; 

  
    public void TogglePausePlay()
    {
        isPaused = !isPaused; 
        Time.timeScale = isPaused ? 0 : 1; 

        // update button image
        button.image.sprite = isPaused ? playSprite : pauseSprite;
    }

}
