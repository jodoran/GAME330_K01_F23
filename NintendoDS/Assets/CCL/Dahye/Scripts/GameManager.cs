using UnityEngine;

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

    // public bool IsGameOver;
    // public bool IsRestart;
    public int Score;

    /// <summary>
    /// 카운트 추가용
    /// </summary>
    public void AddScore(int score)
    {
        this.Score += score;
        Debug.Log(Score); // 현재 스코어 합산 출력 
    }

    private void UIRestartMassage() // 게임 결과 및 재시작 UI 
    {

    }

    public bool IsGameOver() // 게임오버 조건
    {


        return false;
    }








}
