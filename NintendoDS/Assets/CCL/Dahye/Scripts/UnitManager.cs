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
    {   // 유닛 생성 함수
        // 유닛 프리팹을 생성 후에 유닛그룹 폴더 안에 넣겠다는 의미.
        // 유닛그룹은 스폰위치를 담당하기도 한다. 

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

        // 랜덤 인덱스 생성
        int randomIndex = UnityEngine.Random.Range(0, unitPrefabs.Length);

        // 랜덤 인덱스에 해당하는 프리팹 반환
        return unitPrefabs[randomIndex];
    }

    void NextUnit() // 다음 유닛 생성 함수
    {
        Unit newUnit = GetUnits(); // 새로운 유닛 생성을 위해 겟유닛 함수 호출


        lastUnitPrefab = newUnit; // 생성된 유닛을 일시 보관


        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        while (lastUnitPrefab != null) // 드롭 완료 이후
        {
            yield return null; // 무한반복 방지. 드롭 이후 나가라

        }
        yield return new WaitForSeconds(2.5f);
        NextUnit();
    }



}
