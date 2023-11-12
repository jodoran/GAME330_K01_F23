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
    public Unit GetUnits()
    {   // ���� ���� �Լ�
        // ���� �������� ���� �Ŀ� ���ֱ׷� ���� �ȿ� �ְڴٴ� �ǹ�.
        // ���ֱ׷��� ������ġ�� ����ϱ⵵ �Ѵ�. 

        GameObject prefabToSpawn = GetRandomUnitPrefab();
        if (prefabToSpawn == null)
            return null;

        GameObject instant = Instantiate(prefabToSpawn, unitGroups);

        Unit instantUnit = instant.GetComponent<Unit>();

        if (instantUnit == null) // ��Ÿ�� ���� ����
        {
            Debug.LogError("Instantiated object does not have a Unit component.");
            return null;
        }

        return instantUnit;
    }
    public GameObject GetRandomUnitPrefab()
    {
        if (mySO == null || mySO.Length == 0)
        {
            Debug.LogError("UnitPrefabs array is not set or empty!");
            return null;
        }

        // ���� �ε��� ����
        int randomIndex = UnityEngine.Random.Range(0, mySO.Length);

        // ���� �ε����� �ش��ϴ� ������ ��ȯ
        return mySO[randomIndex].UnitPrefabs;
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
        yield return new WaitForSeconds(1.5f);
        NextUnit();
    }



}
