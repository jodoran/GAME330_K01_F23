using UnityEngine;
using UnityEngine.UI;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    /*  Score 
     *  GameOver Condition 
     *  Restart 
     *  - void AddScore(int score)
        - ī��Ʈ �߰���
        - bool IsGameOver( )
        - ���� �����Ȳ ���� �Ǵ�
        - void Restart(bool isRestart)
        - �� ���ΰ� ���� �±� �浹 ��  ���ӿ��� ���� (after job)
     */

    // public bool IsRestart;
    public bool IsGameOver;
    public int Score;


    public Text scoreText; // Unity Inspector���� �Ҵ��� UI Text ���
    /// <summary>
    /// ī��Ʈ �߰���
    /// </summary>

    public void AddScore(int score)
    {
        this.Score += score;
        Debug.Log(Score); // ���� ���ھ� �ջ� ��� 
        UpdateScoreUI(); // UI Text ������Ʈ ȣ��
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "[" + Score.ToString() + "]"; // Score�� UI Text�� ǥ��
        }
    }

    private void UIRestartMassage() // ���� ��� �� ����� UI 
    {

    }

    public void GameOver() // ���ӿ��� ����
    {
        if (IsGameOver)
            return;
        IsGameOver = true;
        Debug.Log("gameover���ӿ���");


    }








}
