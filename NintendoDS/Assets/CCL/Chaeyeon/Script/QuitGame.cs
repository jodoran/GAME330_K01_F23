using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� ���ø����̼ǿ����� ���ø����̼� ����
        Application.Quit();
#endif
    }
}
