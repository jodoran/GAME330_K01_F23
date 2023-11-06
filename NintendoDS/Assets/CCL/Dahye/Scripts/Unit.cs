using System;
using UnityEngine;

public class Unit : MonoBehaviour
/* �̵��� ����� ����� ���� ������ �ൿ ��ũ��Ʈ
 * This script for : 
 * left & right movement behaviour
 * subscribe events <Drop>
*/

{

    //[SerializeField] private Vector2 thisObj;
    [SerializeField]
    private float speed = 3.0f;

    private bool isMove;
    private bool inBox;


    Rigidbody2D rigid;
    //Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.simulated = false;
        isMove = true;
        inBox = false;
        // OnEnable(); // Event Subscription ON! 

        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetButtonDown(GameManager.Instance.Akey))
        //    GameManager.Instance.AButtonPressed();
    }

    void FixedUpdate() //�������� Ű �Է��� FixedUpdate
    {
        horizontalMove();
    }
    void horizontalMove()
    {
        if (isMove && inBox == false)
        {
            var movement = Input.GetAxis(GameManager.Instance.horizontal);
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        }
    }


    //-----------Event Subscribe-----------------------------------

    private void OnEnable()
    {
        GameManager.Instance.OnAbuttonPressed += HandleAbuttonPressed;
        Debug.Log("���� ��");
    }
    private void OnDisable() // AŰ ����� ������ �� ��� 
    {
        GameManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
        Debug.Log("���� ����");
    }
    public void HandleAbuttonPressed(object sender, EventArgs e) // A Ű ������ �� �Ұǵ�?
    {
        // ��� �Ϸ��� ������Ʈ�� �־���ϴµ�, null �̶�� return���� ������, �����ް� �ٽÿͶ�.
        if (GameManager.Instance.lastUnitPrefab == null)
            return;
        Drop();

        Debug.Log("�̺�Ʈ ��� �� Null ó�� �� ");
    }
    public void Drop()
    {
        isMove = false;
        rigid.simulated = true;
        Debug.Log("��� �ߵ�");
        GameManager.Instance.lastUnitPrefab = null;
        //������Ʈ ��� sfx �߰�
    }





}






//  ***Study Notes***
// [TODO] ����� �Ŀ��� �̵� ��Ʈ�� �Ұ� ���� �߰�
//  - Ư�� range �ȿ����� �̵� ���� -> rigidbody simulation �¿����� �ذ�! 
//  - �ڽ� range �ȿ� ���� �� �̵� �Ұ���
//  - Screen tap printer ���� ��Ÿ�Ϸ� ��� �� ��ǲ���� ��ȭ�ϴ� ��?


