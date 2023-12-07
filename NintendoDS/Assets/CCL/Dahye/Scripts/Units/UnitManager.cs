using System.Collections;
using UnityEngine;

public class UnitManager : SingletonMonoBehaviour<UnitManager>
{
    [SerializeField] private Transform unitGroups;
    [SerializeField] private Unit myUnit;
    [SerializeField] private UnitScriptableObject[] mySO;

    public int maxLevel;
    private bool isDropped = true;

    void Start()
    {
        Debug.Log("Scriptable Object Length : " + mySO.Length);
        if (!GameManager.Instance.IsGameOver)
            nextUnit();
    }

    /// <summary>
    /// 유닛을 머지할 때 호출되는 함수
    /// </summary>
    /// <param name="unitLevel"></param>
    public void MergeComplate(UnitLevel unitLevel, Vector3 posotion)
    {
        var nextLevelPrefab = LevelPrefab(unitLevel);
        if (nextLevelPrefab == null)
            Debug.Log("next level prefab is null");

        var nextLevelUnit = Instantiate(nextLevelPrefab, posotion, Quaternion.identity).GetComponent<Unit>();
        if (nextLevelUnit == null)
            Debug.Log("level up routine next null");

        nextLevelUnit.Init(unitLevel, true);

        //Score
        GameManager.Instance.AddScore((int)Mathf.Pow(levelScore(unitLevel), 1));
    }

    /// <summary>
    /// 드롭이 끝낫을 때 호출되는 함수
    /// </summary>
    public void DropComplete()
    {
        StartCoroutine(PauseFunctionForSeconds(0.5f));
    }

    private IEnumerator PauseFunctionForSeconds(float seconds)
    {
        // 함수 실행 전에 정지 대기
        yield return new WaitForSeconds(seconds);

        // 일정 시간이 지난 후에 함수 실행
        this.isDropped = true;
        Debug.Log("Drop Complate");
    }

    /// <summary>
    /// 레벨에 맞는 게임 오브젝트 생성
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public GameObject LevelPrefab(UnitLevel level)
    {
        return level > UnitLevel.Level11 ?
               mySO[(int)UnitLevel.Level11].UnitPrefabs : mySO[(int)level].UnitPrefabs;
    }


    /// <summary>
    /// 레벨에 맞는 게임 스코어 생성
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int levelScore(UnitLevel level)
    {
        return mySO[(int)level].Score;
    }

    /// <summary>
    /// 다음에 나올 유닛의 인덱스를 생성
    /// </summary>
    /// <returns></returns>
    private int getNextUnitLevelIndex()
    {
        return UnityEngine.Random.Range(0, Mathf.Min(maxLevel, 5));
    }
    /// <summary>
    /// 유닛 프리펩을 생성
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private GameObject getUnitPrefab(int index)
    {
        return this.mySO[index].UnitPrefabs;
    }

    /// <summary>
    /// 유닛을 생성
    /// </summary>
    /// <returns></returns>
    private Unit getUnit()
    {
        int unitRandomIndex = getNextUnitLevelIndex();
        if (unitRandomIndex == -1)
            return null;

        this.isDropped = false;

        // ...
        var unitPrefabs = getUnitPrefab(unitRandomIndex);

        var unitInstant = Instantiate(unitPrefabs, unitGroups).GetComponent<Unit>();
        if (unitInstant == null) return null;

        unitInstant.Init(this.mySO[unitRandomIndex].UnitLevel, false);      // 랜덤 인덱스로 계산된 (생성된 unitPrefabs의) 레벨 타입을 Init 함수에 전달합니다.
        return unitInstant;
    }

    /// <summary>
    /// 유닛 생성
    /// </summary>
    private void nextUnit()
    {
        Unit unit = getUnit();

        StartCoroutine("waitNext");
    }

    private IEnumerator waitNext()
    {
        Debug.Log("waitNext :" + this.isDropped);
        while (!this.isDropped)
            yield return new WaitForSeconds(0.5f);

        nextUnit();
    }
}
