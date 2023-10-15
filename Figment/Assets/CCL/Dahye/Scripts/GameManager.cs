//GameManager.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive { get { return isGameActive; } } //외부 접근 bool
    private bool isGameActive = true;    
    private static GameManager _instance;


    public float gameMinute = 2f; //2mins
    private void Start()
    {
        //씬전환 중에 자꾸 일시정지가 되서 "Time.timeScale =1;추가했어요!
        Time.timeScale = 1;
        StartCoroutine(GameOverAfterTime(gameMinute * 60f));
    }

    IEnumerator GameOverAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        GameOver();
    }




    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }
    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("Game Over!"); // 게임 오버 로직
        // 예를 들어, SceneManager.LoadScene("GameOverScene"); 등
    }
}
