using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChangeSceneAfterTime(10, "loading2"));
    }

    IEnumerator ChangeSceneAfterTime(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}

