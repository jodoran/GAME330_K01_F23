using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour, IDSTapListener
{
    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Game is exiting");
    }

   

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        if (DSTapRouter.RectangleContainsDSPoint(GetComponent<RectTransform>(), tapPosition))
        {
            ExitGame(); // ���� ���� �޼ҵ� ȣ��
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
