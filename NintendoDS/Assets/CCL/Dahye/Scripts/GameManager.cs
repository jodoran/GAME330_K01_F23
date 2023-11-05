using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //-------------외부참조-----------------------
    private static GameManager _instance; // 내부
    public static GameManager Instance // 외부
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    Debug.Log("no Singleton obj");
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
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
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.

    }

    //---------------------------------------------










    //Unit Prefabs
    //public GameObject[] unitPrefabsArray;
    //var randomSpawn = Random.Range(0, unitPrefabsArray.length);
    public GameObject unitPrefabs;

    public Unit lastUnit;
    public Transform unitGroups;

    //Event Handler
    public event EventHandler OnAbuttonPressed;



    void Start()
    {
        NextUnit();
    }

    Unit GetUnits()
    {   // 유닛 생성 함수
        // 유닛 프리팹을 생성 후에 유닛그룹 폴더 안에 넣겠다는 의미.
        // 유닛그룹은 스폰위치를 담당하기도 한다. 
        GameObject instant = Instantiate(unitPrefabs, unitGroups);
        Unit instantUnit = instant.GetComponent<Unit>();
        return instantUnit;
    }

    void NextUnit()
    {
        // 다음 유닛 생성 함수
        Unit newUnit = GetUnits(); // 새로운 유닛 생성을 위해 겟유닛 함수 호출
        lastUnit = newUnit; // 생성된 유닛을 일시 보관

        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        while (lastUnit != null)
        {
            yield return null;

        }
        yield return new WaitForSeconds(2.5f);
        NextUnit();
    }




    public void AButtonPressed()
    {
        OnAbuttonPressed?.Invoke(this, EventArgs.Empty);
        if (OnAbuttonPressed == null)
            Debug.Log("No subscribers for event");
    }



}
