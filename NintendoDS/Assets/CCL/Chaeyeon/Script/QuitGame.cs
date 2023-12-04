using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 실제 애플리케이션에서는 애플리케이션 종료
        Application.Quit();
#endif
    }
}
