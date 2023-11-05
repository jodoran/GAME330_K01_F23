using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /* 이동과 드롭의 기능을 가진 유닛의 행동 스크립트
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






        // [TODO] 드롭한 후에는 이동 컨트롤 불가 로직 추가
        //  - 특정 range 안에서만 이동 가능 -> rigidbody simulation 온오프로 해결! 
        //  - 박스 range 안에 감지 후 이동 불가능
        //  - Screen tap printer 에서 스타일러 사용 시 인풋설정 변화하는 법?


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
        if (GameManager.Instance.lastUnit == null) // 드롭을 하기위해선 오브젝트가 있어야하는데, 생성된 오브젝트가 null 이라면 return으로 나가고, 생성받고 와라.
            return;

        Debug.Log("HandleAbuttonPressed method called.");
        Drop();
        GameManager.Instance.lastUnit = null;


        Debug.Log("이벤트 드롭 마무리");
    }
    public void Drop()
    {
        isMove = false;
        rigid.simulated = true;
        Debug.Log("유닛 드롭 발동");
        //오브젝트 드롭 sfx 추가
    }





}

