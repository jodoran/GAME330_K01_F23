using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc : MonoBehaviour
{
    void Start()
    {
        // GameManager의 인스턴스 얻기
        GameManager gameManager = GameManager.Instance;

        // GameManager의 Score 출력
        if (gameManager != null)
        {
            Debug.Log("Score: " + gameManager.Score);
        }
        else
        {
            Debug.LogWarning("GameManager instance not found.");
        }
    }

}
