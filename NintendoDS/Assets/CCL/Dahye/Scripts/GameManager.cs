using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*  Score 
     *  GameOver Condition 
     *  Restart 
     */

    public bool isGameOver;


    //-------------외부참조-----------------------

    private static GameManager _instance; // 내부
    public static GameManager Instance // 외부
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Debug.Log("게임매니저 인스턴스 중복! 삭제하겠습니당~");
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.

    }
}
