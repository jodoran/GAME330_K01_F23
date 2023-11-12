using System.Collections;
using UnityEngine;

public class UnitManager : SingletonMonoBehaviour<UnitManager>
{
    /*  Unit Management
     *  Object Pool
     *  Add unit / Remove unit
     *  New obj spawn                               ^^ 
     *  Merge obj spawn         
     *  New spawn random calculation with ratio 
     */

    // ���� ���� ��� �� ���� ���� ���Ű���

    [SerializeField] private Transform unitGroups;

    [SerializeField] private Unit myUnit;
    [SerializeField] private UnitScriptableObject[] mySO;

    public Unit lastUnitPrefab;
    //[SerializeField, HideInInspector]

    void Start()
    {
        //GameManager.Instance.isGameOver = false;
        //if (!GameManager.Instance.isGameOver)
        NextUnit();
    }
    private int getNextUnitTypeIndex()
    {
        if (mySO == null || mySO.Length == 0)
        {
            Debug.LogError("UnitPrefabs array is not set or empty!");
            return -1; // == null ����
        }

        return 0;

        return UnityEngine.Random.Range(0, mySO.Length);
    }
    /// <summary>
    /// ���� �ε����� �°� ������ ��ȯ
    /// </summary>
    /// <param name="index">���� �ε���</param>
    /// <returns></returns>
    private GameObject getUnitPrefab(int index)
    {
        return this.mySO[index].UnitPrefabs;
    }

    /// <summary>
    /// ���� ���� �� Unit ������Ʈ�� �־��ݴϴ�.
    /// </summary>
    /// <returns></returns>
    private Unit getUnit()
    {
        int unitRandomIndex = getNextUnitTypeIndex();
        if (unitRandomIndex == -1)
            return null;

        var unitPrefabs = getUnitPrefab(unitRandomIndex);

        var unitInstant = Instantiate(unitPrefabs, unitGroups).GetComponent<Unit>();
        if (unitInstant == null)
            return null;

        unitInstant.Init(false);

        // ������ ���� �������� ����
        unitInstant.Level = this.mySO[unitRandomIndex].UnitLevel;
        Debug.Log(unitInstant);
        return unitInstant;
    }
    public Unit GetUnits()
    {   // ���� ���� �Լ�
        // ���� �������� ���� �Ŀ� ���ֱ׷� ���� �ȿ� �ְڴٴ� �ǹ�.
        // ���ֱ׷��� ������ġ�� ����ϱ⵵ �Ѵ�. 
        if (mySO == null || mySO.Length == 0)
        {
            Debug.LogError("UnitPrefabs array is not set or empty!");
            return null;
        }

        // ���� �ε��� ����
        int randomIndex = UnityEngine.Random.Range(0, mySO.Length);

        GameObject prefabToSpawn = mySO[randomIndex].UnitPrefabs;
        if (prefabToSpawn == null)
            return null;

        GameObject instant = Instantiate(prefabToSpawn, unitGroups);

        Unit instantUnit = instant.GetComponent<Unit>();

        instantUnit.Level = mySO[randomIndex].UnitLevel;

        if (instantUnit == null) // ��Ÿ�� ���� ����
        {
            Debug.LogError("Instantiated object does not have a Unit component.");
            return null;
        }

        return instantUnit;
    }

    void NextUnit() // ���� ���� ���� �Լ�
    {
        lastUnitPrefab = getUnit(); // ������ ������ �Ͻ� ����


        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        while (lastUnitPrefab != null) // ��� �Ϸ� ����
        {
            yield return null; // ���ѹݺ� ����. ��� ���� ������

        }
        yield return new WaitForSeconds(1.5f);
        NextUnit();
    }
    // ���Ġ���(��ġ, Ÿ��)

    public GameObject LevelPrefab(UnitLevel level)
    {
        return mySO[(int)level].UnitPrefabs;
    }

}
