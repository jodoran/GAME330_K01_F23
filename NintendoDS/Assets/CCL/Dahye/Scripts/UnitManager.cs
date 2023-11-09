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

    [SerializeField] private GameObject[] unitPrefabs;
    [SerializeField] private Transform unitGroups;

    public Unit lastUnitPrefab;
    public Unit myUnit;


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
