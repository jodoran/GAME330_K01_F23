using UnityEngine;
using UnityEngine.UI;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    /*  Score 
     *  GameOver Condition 
     *  Restart 
     *  - void AddScore(int score)
        - 카운트 추가용
        - bool IsGameOver( )
        - 게임 진행상황 여부 판단
        - void Restart(bool isRestart)
        - 선 라인과 유닛 태그 충돌 시  게임오버 진행 (after job)
     */

    // public bool IsRestart;
    public bool IsGameOver;
    public int Score;


    public Text scoreText; // Unity Inspector에서 할당할 UI Text 요소
    /// <summary>
    /// 카운트 추가용
    /// </summary>

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
            scoreText.text = "[" + Score.ToString() + "]"; // Score를 UI Text에 표시
        }
    }

    private void UIRestartMassage() // 게임 결과 및 재시작 UI 
    {

    }

    public void GameOver() // 게임오버 조건
    {
        if (IsGameOver)
            return;
        IsGameOver = true;
        Debug.Log("gameover게임오버");


    }








}
