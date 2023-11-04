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
            //이벤트 핸들러 실행 코드 추후변경
            Drop();
            isMove = false;
            inBox = true;
        }
    }

    //지속적인 키 입력은 FixedUpdate에서 하는 것
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






        // [TODO] 드롭한 후에는 이동 컨트롤 불가 로직 추가
        //  - 특정 range 안에서만 이동 가능 -> rigidbody simulation 온오프로 해결! 
        //  - 박스 range 안에 감지 후 이동 불가능
        //  - Screen tap printer 에서 스타일러 사용 시 인풋설정 변화하는 법?


    }
    void Drop()
    {
        rigid.simulated = true;
        //오브젝트 드롭 sfx 추가

    }

}

