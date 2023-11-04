using UnityEngine;

public class Movement : MonoBehaviour
{
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
            //�̺�Ʈ �ڵ鷯 ���� �ڵ� ���ĺ���
            Drop();
            isMove = false;
            inBox = true;
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

        //Restrict movement over borders
        float leftBorder = -0.56f;
        float rightBorger = 0.56f;






        // [TODO] ����� �Ŀ��� �̵� ��Ʈ�� �Ұ� ���� �߰�
        //  - Ư�� range �ȿ����� �̵� ���� -> rigidbody simulation �¿����� �ذ�! 
        //  - �ڽ� range �ȿ� ���� �� �̵� �Ұ���
        //  - Screen tap printer ���� ��Ÿ�Ϸ� ��� �� ��ǲ���� ��ȭ�ϴ� ��?


    }
    void Drop()
    {
        rigid.simulated = true;
        //������Ʈ ��� sfx �߰�

    }

}

