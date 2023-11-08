using System.Collections;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    /*  Unit Management
     *  Object Pool
     *  Add unit / Remove unit
     *  New obj spawn                               ^^ 
     *  Merge obj spawn         
     *  New spawn random calculation with ratio 
     */
    //-------------�ܺ�����-----------------------

    private static UnitManager _instance; // ����
    public static UnitManager Instance // �ܺ�
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindObjectOfType<UnitManager>();
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
        else if (_instance != this)
        {
            Debug.Log("���ָŴ��� �ν��Ͻ� �ߺ�! �����ϰڽ��ϴ�~");
            Destroy(gameObject);
        }
    }

    //--------------------------------------




    void Start()
    {
        GameManager.Instance.isGameOver = false;
        if (!GameManager.Instance.isGameOver)
            NextUnit();
    }
    //---------------------------------------------
    // ���� ���� ��� �� ���� ���� ���Ű���

    [SerializeField] private GameObject[] unitPrefabs;
    [SerializeField] private Transform unitGroups;

    public Unit lastUnitPrefab;
    public Unit myUnit;

    public Unit GetUnits()
    {   // ���� ���� �Լ�
        // ���� �������� ���� �Ŀ� ���ֱ׷� ���� �ȿ� �ְڴٴ� �ǹ�.
        // ���ֱ׷��� ������ġ�� ����ϱ⵵ �Ѵ�. 

        GameObject prefabToSpawn = GetRandomUnitPrefab();

        GameObject instant = Instantiate(prefabToSpawn, unitGroups);

        Unit instantUnit = instant.GetComponent<Unit>();

        return instantUnit;
    }
    public GameObject GetRandomUnitPrefab()
    {
        if (unitPrefabs == null || unitPrefabs.Length == 0)
        {
            Debug.LogError("UnitPrefabs array is not set or empty!");
            return null;
        }

        // ���� �ε��� ����
        int randomIndex = UnityEngine.Random.Range(0, unitPrefabs.Length);

        // ���� �ε����� �ش��ϴ� ������ ��ȯ
        return unitPrefabs[randomIndex];
    }

    void NextUnit() // ���� ���� ���� �Լ�
    {
        Unit newUnit = GetUnits(); // ���ο� ���� ������ ���� ������ �Լ� ȣ��


        lastUnitPrefab = newUnit; // ������ ������ �Ͻ� ����


        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        while (lastUnitPrefab != null) // ��� �Ϸ� ����
        {
            yield return null; // ���ѹݺ� ����. ��� ���� ������

        }
        yield return new WaitForSeconds(2.5f);
        NextUnit();
    }



}
