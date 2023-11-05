using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //-------------�ܺ�����-----------------------
    private static GameManager _instance; // ����
    public static GameManager Instance // �ܺ�
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.

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
    {   // ���� ���� �Լ�
        // ���� �������� ���� �Ŀ� ���ֱ׷� ���� �ȿ� �ְڴٴ� �ǹ�.
        // ���ֱ׷��� ������ġ�� ����ϱ⵵ �Ѵ�. 
        GameObject instant = Instantiate(unitPrefabs, unitGroups);
        Unit instantUnit = instant.GetComponent<Unit>();
        return instantUnit;
    }

    void NextUnit()
    {
        // ���� ���� ���� �Լ�
        Unit newUnit = GetUnits(); // ���ο� ���� ������ ���� ������ �Լ� ȣ��
        lastUnit = newUnit; // ������ ������ �Ͻ� ����

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
