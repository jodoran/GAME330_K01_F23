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
        Debug.Log("Game Over!"); // 게임 오버 로직
        // 예를 들어, SceneManager.LoadScene("GameOverScene"); 등
    }
}
