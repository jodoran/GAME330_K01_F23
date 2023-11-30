using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc : MonoBehaviour
{
    void Start()
    {
        // GameManager�� �ν��Ͻ� ���
        GameManager gameManager = GameManager.Instance;

        // GameManager�� Score ���
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
