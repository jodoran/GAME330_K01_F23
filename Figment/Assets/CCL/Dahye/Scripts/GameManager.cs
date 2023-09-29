using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameMinute = 2f; //2mins
    private void Start()
    {
        StartCoroutine(GameOverAfterTime(gameMinute * 60f));
    }

    IEnumerator GameOverAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        GameOver();
    }

    void GameOver()
    {
        Debug.Log("Game Over!"); // ���� ���� ����
        // ���� ���, SceneManager.LoadScene("GameOverScene"); ��
    }
}
