using UnityEngine;
using UnityEngine.UI;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool IsGameOver;
    public int Score;



    public Text scoreText;
    public Text scoreText2;// Unity Inspector에서 할당할 UI Text 요소
    public UIanim UIanim;
    public Unit Unit;

    private void Awake()
    {
        //Application.targetFrameRate = 60;
    }

    public void AddScore(int score)
    {
        this.Score += score;
        Debug.Log(Score); // 현재 스코어 합산 출력 
        UpdateScoreUI(); // UI Text 업데이트 호출
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = Score.ToString(); // Score를 UI Text에 표시
        }

        if (scoreText2 != null)
        {
            scoreText2.text = Score.ToString(); // Score를 UI Text에 표시
        }
    }

    public void GameOver() // 게임오버 조건
    {
        //if (IsGameOver)
        //    return;
        IsGameOver = true;
        UIanim.GameOverUI();
        Debug.Log("gameover게임오버");

    }









}
