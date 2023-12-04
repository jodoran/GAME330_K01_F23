using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HowToPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSceneAfterTime(6, "MainScene"));
    }

    IEnumerator ChangeSceneAfterTime(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}
