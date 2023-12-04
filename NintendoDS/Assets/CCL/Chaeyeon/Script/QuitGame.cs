using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour, IDSTapListener
{
    public void OnScreenTapDown(Vector2 tapPosition)
    {
        // ȭ���� �ǵǾ��� �� ���� ����
        ExitGame();
    }

    public void ExitGame()
    {
        
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        // ���� ���ø����̼ǿ����� ���ø����̼� ����
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
