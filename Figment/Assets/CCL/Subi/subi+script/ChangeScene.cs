using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    //FigmentInput Fig = GameObject.Find("FigmentInput").GetComponent<FigmentInput>();
    //bool IsPause;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
    }
    public void scenechange()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            
            SceneManager.LoadScene("FigmentTestScene");
            //SceneManager.LoadScene(1);
            //Time.timeScale = 1;

        }

        
    }
}
