using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*  Score 
     *  GameOver Condition 
     *  Restart 
     */

    public bool isGameOver;


    //-------------�ܺ�����-----------------------

    private static GameManager _instance; // ����
    public static GameManager Instance // �ܺ�
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (_instance is null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Debug.Log("���ӸŴ��� �ν��Ͻ� �ߺ�! �����ϰڽ��ϴ�~");
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.

    }
}
