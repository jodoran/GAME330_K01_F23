using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChangeSceneAfterTime(3, "Home"));
    }

    IEnumerator ChangeSceneAfterTime(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}

