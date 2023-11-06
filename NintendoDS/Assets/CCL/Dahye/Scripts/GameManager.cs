using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
/*This script is for :
 * Input system provider
 * Event Handler 제공자 setting
 */
{
    //-------------외부참조-----------------------

    private static GameManager _instance; // 내부
    public static GameManager Instance // 외부
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (_instance is null)
            {
                _instance = FindObjectOfType<GameManager>();
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
            Debug.Log("게임매니저 인스턴스 중복! 삭제하겠습니당~");
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되                                                                                                                                                                                                더라도 선언되었던 인스턴스가 파괴되지 않는다.

    }
    //--------------------------------------
    //Key Inputs
    public string Akey = "Fire2";
    public string Bkey = "Fire1";
    public string Ykey = "Fire3";
    public string Xkey = "Jump";
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";

    //---------------------------------------------
    //이벤트 핸들러
    public event EventHandler OnAbuttonPressed;

    public GameObject unitPrefabs;

    public Unit lastUnitPrefab;
    public Transform unitGroups;

    void Start()
    {
        NextUnit();
    }
    void Update()
    {
        AButtonPressed();
    }

    Unit GetUnits()
    {   // 유닛 생성 함수
        // 유닛 프리팹을 생성 후에 유닛그룹 폴더 안에 넣겠다는 의미.
        // 유닛그룹은 스폰위치를 담당하기도 한다. 
        GameObject instant = Instantiate(unitPrefabs, unitGroups);
        Unit instantUnit = instant.GetComponent<Unit>();
        return instantUnit;
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




    public void AButtonPressed()
    {
        if (Input.GetButtonDown(Akey))
        {
            OnAbuttonPressed?.Invoke(this, EventArgs.Empty);
            if (OnAbuttonPressed == null) // 이벤트가 NULL 이면 
                Debug.Log("No subscribers for event");
        }
    }
}
