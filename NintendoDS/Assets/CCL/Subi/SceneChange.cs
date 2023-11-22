using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
 
    public void Scene1()
    {
        SceneManager.LoadScene("Nintendo");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("Scene3");
    }
}   

