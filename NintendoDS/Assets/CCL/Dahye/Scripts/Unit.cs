using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /* �̵��� ����� ����� ���� ������ �ൿ ��ũ��Ʈ
    */

    //--------------------------------------
    //Key Inputs
    private string Akey = "Fire2";
    private string Bkey = "Fire1";
    private string Ykey = "Fire3";
    private string Xkey = "Jump";
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";
    //--------------------------------------
    //[SerializeField] private Vector2 thisObj;
    [SerializeField] private float speed = 3.0f;

    private bool isMove;
    private bool inBox;


    Rigidbody2D rigid;
    //Animator anim;

    public event EventHandler OnAbuttonPressed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.simulated = false;
        isMove = true;
        inBox = false;
        //anim = GetComponent<Animator>();

    }

    void Update()
    {

        if (Input.GetButtonDown(Akey))
        {
            //OnAbuttonPressed?.Invoke(this, EventArgs.Empty);
            GameManager.Instance.AButtonPressed();
        }

    }

    //�������� Ű �Է��� FixedUpdate���� �ϴ� ��
    void FixedUpdate()
    {
        horizontalMove();
    }

    void horizontalMove()
    {
        if (isMove && inBox == false)
        {
            var movement = Input.GetAxis(horizontal);
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        }
        else
        { }






        // [TODO] ����� �Ŀ��� �̵� ��Ʈ�� �Ұ� ���� �߰�
        //  - Ư�� range �ȿ����� �̵� ���� -> rigidbody simulation �¿����� �ذ�! 
        //  - �ڽ� range �ȿ� ���� �� �̵� �Ұ���
        //  - Screen tap printer ���� ��Ÿ�Ϸ� ��� �� ��ǲ���� ��ȭ�ϴ� ��?


    }
    private void OnEnable()
    {
        GameManager.Instance.OnAbuttonPressed += HandleAbuttonPressed;
        Debug.Log("Subscribed to OnAbuttonPressed event.");
    }
    private void OnDisable()
    {
        GameManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
    }
    private void HandleAbuttonPressed(object sender, EventArgs e)
    {
        if (GameManager.Instance.lastUnit == null) // ����� �ϱ����ؼ� ������Ʈ�� �־���ϴµ�, ������ ������Ʈ�� null �̶�� return���� ������, �����ް� �Ͷ�.
            return;

        Debug.Log("HandleAbuttonPressed method called.");
        Drop();
        GameManager.Instance.lastUnit = null;


        Debug.Log("�̺�Ʈ ��� ������");
    }
    public void Drop()
    {
        isMove = false;
        rigid.simulated = true;
        Debug.Log("���� ��� �ߵ�");
        //������Ʈ ��� sfx �߰�
    }





}

