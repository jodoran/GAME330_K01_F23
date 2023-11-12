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

    // 유닛 랜덤 계산 및 생성 관리 제거관리

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
            return -1; // == null 실패
        }

        return 0;

        return UnityEngine.Random.Range(0, mySO.Length);
    }
    /// <summary>
    /// 랜덤 인덱스에 맞게 프리팹 반환
    /// </summary>
    /// <param name="index">랜덤 인덱스</param>
    /// <returns></returns>
    private GameObject getUnitPrefab(int index)
    {
        return this.mySO[index].UnitPrefabs;
    }

    /// <summary>
    /// 유닛 생성 후 Unit 컴포넌트도 넣어줍니다.
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

        // 생성한 랜덤 프리팹의 레벨
        unitInstant.Level = this.mySO[unitRandomIndex].UnitLevel;
        Debug.Log(unitInstant);
        return unitInstant;
    }
    public Unit GetUnits()
    {   // 유닛 생성 함수
        // 유닛 프리팹을 생성 후에 유닛그룹 폴더 안에 넣겠다는 의미.
        // 유닛그룹은 스폰위치를 담당하기도 한다. 
        if (mySO == null || mySO.Length == 0)
        {
            Debug.LogError("UnitPrefabs array is not set or empty!");
            return null;
        }

        // 랜덤 인덱스 생성
        int randomIndex = UnityEngine.Random.Range(0, mySO.Length);

        GameObject prefabToSpawn = mySO[randomIndex].UnitPrefabs;
        if (prefabToSpawn == null)
            return null;

        GameObject instant = Instantiate(prefabToSpawn, unitGroups);

        Unit instantUnit = instant.GetComponent<Unit>();

        instantUnit.Level = mySO[randomIndex].UnitLevel;

        if (instantUnit == null) // 런타임 오류 방지
        {
            Debug.LogError("Instantiated object does not have a Unit component.");
            return null;
        }

        return instantUnit;
    }

    void NextUnit() // 다음 유닛 생성 함수
    {
        lastUnitPrefab = getUnit(); // 생성된 유닛을 일시 보관


        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        while (lastUnitPrefab != null) // 드롭 완료 이후
        {
            yield return null; // 무한반복 방지. 드롭 이후 나가라

        }
        yield return new WaitForSeconds(1.5f);
        NextUnit();
    }
    // 합쳐졋어(위치, 타입)

    public GameObject LevelPrefab(UnitLevel level)
    {
        return mySO[(int)level].UnitPrefabs;
    }

}
