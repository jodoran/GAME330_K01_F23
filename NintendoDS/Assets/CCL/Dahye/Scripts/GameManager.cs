using UnityEngine;

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

    // public bool IsGameOver;
    // public bool IsRestart;
    public int Score;

    /// <summary>
    /// ī��Ʈ �߰���
    /// </summary>
    public void AddScore(int score)
    {
        this.Score += score;
        Debug.Log(Score); // ���� ���ھ� �ջ� ��� 
    }

    private void UIRestartMassage() // ���� ��� �� ����� UI 
    {

    }

    public bool IsGameOver() // ���ӿ��� ����
    {


        return false;
    }








}
