using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour, IDSTapListener
{
    public string sceneName;


    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
        {
            SceneManager.LoadScene(sceneName);

        }
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {
      //  throw new System.NotImplementedException();
    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
    //throw new System.NotImplementedException();
    }
}