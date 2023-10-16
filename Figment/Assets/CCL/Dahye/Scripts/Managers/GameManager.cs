//GameManager.cs
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 총 라운드
    /// </summary> 
    [Tooltip("총 라운드")]
    [SerializeField] private int round = 2;

    [Tooltip("각 라운드 시간")]
    [SerializeField] private float roundTime = 30f;

    [Tooltip("쉬는 시간")]
    [SerializeField] private float roundBreakTime = 10f;

    [SerializeField] private AudioClip gameOverBGM;
    [SerializeField] private AudioClip gameWinBGM;


    public GameObject bigwaveText;
    public GameObject finalwaveText;
    public GameObject gameoverText;

    public Vector3 position = new Vector3(-2, 24, 0);
    public Quaternion rotation = Quaternion.identity;
    /// <summary>
    /// 현재 라운드
    /// </summary>
    private int currentRound = 0;
    public int CurrentRound { get { return currentRound; } }

    /// <summary>
    /// 쉬는시간인지 여부
    /// </summary>
    private bool isBreak = false;
    public bool IsBreak { get { return isBreak; } }

    /// <summary>
    /// 게임이 진행중인지
    /// </summary>
    private bool isGameActive = true;
    public bool IsGameActive { get { return isGameActive; } }

    /// <summary>
    /// 게임 스타뚜!
    /// </summary>
    private void Start()
    {
        Debug.Log("게임 스타트");
        StartCoroutine(Round0());
    }

    IEnumerator Round0()
    {
        Debug.Log("라운드 시작 : " + this.currentRound);

        this.isBreak = false;
        yield return new WaitForSeconds(this.roundTime); // 30
        this.isBreak = true;


        GameObject text = Instantiate(bigwaveText, position, rotation);
        yield return new WaitForSeconds(this.roundBreakTime); // 10
        this.isBreak = false;
        Destroy(text);

        Debug.Log("라운드 끝 : " + this.currentRound);

        this.currentRound += 1;
        StartCoroutine(Round1());

    }
    IEnumerator Round1()
    {
        Debug.Log("라운드 시작 : " + this.currentRound);

        this.isBreak = false;
        yield return new WaitForSeconds(this.roundTime); // 30
        this.isBreak = true;


        GameObject text = Instantiate(finalwaveText, position, rotation);
        yield return new WaitForSeconds(this.roundBreakTime); // 10
        this.isBreak = false;
        Destroy(text);

        Debug.Log("라운드 끝 : " + this.currentRound);
        this.currentRound += 1;
        StartCoroutine(FinalRound2());
    }
    IEnumerator FinalRound2()
    {
        Debug.Log("라운드 시작 : " + this.currentRound);

        this.isBreak = false;
        yield return new WaitForSeconds(this.roundTime); // 30
        this.isBreak = true;

        yield return new WaitForSeconds(this.roundBreakTime); // 10
        this.isBreak = false;

        Debug.Log("라운드 끝 : " + this.currentRound);

        this.isGameActive = false;
        GameOver();
        GameObject text = Instantiate(gameoverText, position, rotation);
        yield return new WaitForSeconds(this.roundBreakTime); // 10
        Destroy(text);

    }

    /// <summary>
    /// 인스턴스에 접근하기 위한 프로퍼티
    /// </summary>
    private static GameManager _instance;
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

    /// <summary>
    /// 게임 오버
    /// </summary>
    public void GameOver()
    {
        //게임이 오버가 호출됐는데 게임오버가 아니거나, 게임오버이지만 에너미가 모두 죽지 않았을 경우
        if (isGameActive || (!isGameActive && !UnitManager.Instance.IsEnemyAllDie()))
        {
            //게임실패
            Debug.Log("Game Over  ! [Score : " + UnitManager.Instance.Score + " ]");
            GameObject text = Instantiate(gameoverText, position, rotation);
            SoundManager.Instance.PlayEffectSound(gameOverBGM);
            SoundManager.Instance.OffBGM();
        }
        else
        {
            //게임성공
            Debug.Log("Game Clear ! [Score : " + UnitManager.Instance.Score + " ]");
            SoundManager.Instance.PlayEffectSound(gameWinBGM);
            SoundManager.Instance.OffBGM();
        }

        // 예를 들어, SceneManager.LoadScene("GameOverScene"); 등
    }
}
