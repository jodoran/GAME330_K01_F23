using UnityEngine;
using UnityEngine.UI;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool IsGameOver;
    public int Score;


    public Text scoreText; // Unity Inspector���� �Ҵ��� UI Text ���

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

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
            scoreText.text = Score.ToString(); // Score�� UI Text�� ǥ��
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
