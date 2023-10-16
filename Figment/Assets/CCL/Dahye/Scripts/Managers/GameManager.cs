//GameManager.cs
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �� ����
    /// </summary> 
    [Tooltip("�� ����")]
    [SerializeField] private int round = 2;

    [Tooltip("�� ���� �ð�")]
    [SerializeField] private float roundTime = 30f;

    [Tooltip("���� �ð�")]
    [SerializeField] private float roundBreakTime = 10f;

    [SerializeField] private AudioClip gameOverBGM;
    [SerializeField] private AudioClip gameWinBGM;


    public GameObject bigwaveText;
    public GameObject finalwaveText;
    public GameObject gameoverText;

    public Vector3 position = new Vector3(-2, 24, 0);
    public Quaternion rotation = Quaternion.identity;
    /// <summary>
    /// ���� ����
    /// </summary>
    private int currentRound = 0;
    public int CurrentRound { get { return currentRound; } }

    /// <summary>
    /// ���½ð����� ����
    /// </summary>
    private bool isBreak = false;
    public bool IsBreak { get { return isBreak; } }

    /// <summary>
    /// ������ ����������
    /// </summary>
    private bool isGameActive = true;
    public bool IsGameActive { get { return isGameActive; } }

    /// <summary>
    /// ���� ��Ÿ��!
    /// </summary>
    private void Start()
    {
        Debug.Log("���� ��ŸƮ");
        StartCoroutine(Round0());
    }

    IEnumerator Round0()
    {
        Debug.Log("���� ���� : " + this.currentRound);

        this.isBreak = false;
        yield return new WaitForSeconds(this.roundTime); // 30
        this.isBreak = true;


        GameObject text = Instantiate(bigwaveText, position, rotation);
        yield return new WaitForSeconds(this.roundBreakTime); // 10
        this.isBreak = false;
        Destroy(text);

        Debug.Log("���� �� : " + this.currentRound);

        this.currentRound += 1;
        StartCoroutine(Round1());

    }
    IEnumerator Round1()
    {
        Debug.Log("���� ���� : " + this.currentRound);

        this.isBreak = false;
        yield return new WaitForSeconds(this.roundTime); // 30
        this.isBreak = true;


        GameObject text = Instantiate(finalwaveText, position, rotation);
        yield return new WaitForSeconds(this.roundBreakTime); // 10
        this.isBreak = false;
        Destroy(text);

        Debug.Log("���� �� : " + this.currentRound);
        this.currentRound += 1;
        StartCoroutine(FinalRound2());
    }
    IEnumerator FinalRound2()
    {
        Debug.Log("���� ���� : " + this.currentRound);

        this.isBreak = false;
        yield return new WaitForSeconds(this.roundTime); // 30
        this.isBreak = true;

        yield return new WaitForSeconds(this.roundBreakTime); // 10
        this.isBreak = false;

        Debug.Log("���� �� : " + this.currentRound);

        this.isGameActive = false;
        GameOver();
        GameObject text = Instantiate(gameoverText, position, rotation);
        yield return new WaitForSeconds(this.roundBreakTime); // 10
        Destroy(text);

    }

    /// <summary>
    /// �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    /// </summary>
    private static GameManager _instance;
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

    /// <summary>
    /// ���� ����
    /// </summary>
    public void GameOver()
    {
        //������ ������ ȣ��ƴµ� ���ӿ����� �ƴϰų�, ���ӿ��������� ���ʹ̰� ��� ���� �ʾ��� ���
        if (isGameActive || (!isGameActive && !UnitManager.Instance.IsEnemyAllDie()))
        {
            //���ӽ���
            Debug.Log("Game Over  ! [Score : " + UnitManager.Instance.Score + " ]");
            GameObject text = Instantiate(gameoverText, position, rotation);
            SoundManager.Instance.PlayEffectSound(gameOverBGM);
            SoundManager.Instance.OffBGM();
        }
        else
        {
            //���Ӽ���
            Debug.Log("Game Clear ! [Score : " + UnitManager.Instance.Score + " ]");
            SoundManager.Instance.PlayEffectSound(gameWinBGM);
            SoundManager.Instance.OffBGM();
        }

        // ���� ���, SceneManager.LoadScene("GameOverScene"); ��
    }
}
