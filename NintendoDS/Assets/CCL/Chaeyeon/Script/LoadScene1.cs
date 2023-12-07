using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene1 : MonoBehaviour, IDSTapListener
{
    public string SceneName;


    public void loadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
        {
            loadScene("MainScene");

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