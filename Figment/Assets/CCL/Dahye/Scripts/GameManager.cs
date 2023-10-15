//GameManager.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive { get { return isGameActive; } } //�ܺ� ���� bool
    private bool isGameActive = true;    
    private static GameManager _instance;


    public float gameMinute = 2f; //2mins
    private void Start()
    {
        //����ȯ �߿� �ڲ� �Ͻ������� �Ǽ� "Time.timeScale =1;�߰��߾��!
        Time.timeScale = 1;
        StartCoroutine(GameOverAfterTime(gameMinute * 60f));
    }

    IEnumerator GameOverAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        GameOver();
    }




    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }
    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("Game Over!"); // ���� ���� ����
        // ���� ���, SceneManager.LoadScene("GameOverScene"); ��
    }
}
