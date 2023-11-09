using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneGameOver : MonoBehaviour
{

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {

            SceneManager.LoadScene(0);
            //SceneManager.LoadScene(1);
            //Time.timeScale = 1;

        }



    }
}
