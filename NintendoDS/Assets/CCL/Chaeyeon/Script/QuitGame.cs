using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour, IDSTapListener
{
    public void OnScreenTapDown(Vector2 tapPosition)
    {
        // 화면이 탭되었을 때 게임 종료
        ExitGame();
    }

    public void ExitGame()
    {
        
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        // 실제 애플리케이션에서는 애플리케이션 종료
        Application.Quit();
    #endif
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
