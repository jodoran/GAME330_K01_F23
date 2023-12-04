using UnityEngine;
using UnityEngine.UI;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool IsGameOver;
    public int Score;


    public Text scoreText; // Unity Inspector���� �Ҵ��� UI Text ���
    public UIanim UIanim;
    public Unit Unit;

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

    public void GameOver() // ���ӿ��� ����
    {
        //if (IsGameOver)
        //    return;
        IsGameOver = true;
        UIanim.GameOverUI();
        Debug.Log("gameover���ӿ���");


    }








}
