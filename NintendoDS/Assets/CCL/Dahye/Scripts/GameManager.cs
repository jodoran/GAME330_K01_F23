using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
/*This script is for :
 * Input system provider
 * Event Handler ������ setting
 */
{
    //-------------�ܺ�����-----------------------

    private static GameManager _instance; // ����
    public static GameManager Instance // �ܺ�
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Debug.Log("���ӸŴ��� �ν��Ͻ� �ߺ�! �����ϰڽ��ϴ�~");
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ��                                                                                                                                                                                                ���� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.

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
    //�̺�Ʈ �ڵ鷯
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
    {   // ���� ���� �Լ�
        // ���� �������� ���� �Ŀ� ���ֱ׷� ���� �ȿ� �ְڴٴ� �ǹ�.
        // ���ֱ׷��� ������ġ�� ����ϱ⵵ �Ѵ�. 
        GameObject instant = Instantiate(unitPrefabs, unitGroups);
        Unit instantUnit = instant.GetComponent<Unit>();
        return instantUnit;
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




    public void AButtonPressed()
    {
        if (Input.GetButtonDown(Akey))
        {
            OnAbuttonPressed?.Invoke(this, EventArgs.Empty);
            if (OnAbuttonPressed == null) // �̺�Ʈ�� NULL �̸� 
                Debug.Log("No subscribers for event");
        }
    }
}
