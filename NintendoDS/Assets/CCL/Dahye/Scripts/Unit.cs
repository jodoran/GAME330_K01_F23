using System;
using UnityEngine;

public class Unit : MonoBehaviour
/* 이동과 드롭의 기능을 가진 유닛의 행동 스크립트
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

    void FixedUpdate() //지속적인 키 입력은 FixedUpdate
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
        Debug.Log("구독 온");
    }
    private void OnDisable() // A키 기능을 제거할 때 사용 
    {
        GameManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
        Debug.Log("구독 오프");
    }
    public void HandleAbuttonPressed(object sender, EventArgs e) // A 키 누르면 뭐 할건데?
    {
        // 드롭 하려면 오브젝트가 있어야하는데, null 이라면 return으로 나가고, 생성받고 다시와라.
        if (GameManager.Instance.lastUnitPrefab == null)
            return;
        Drop();

        Debug.Log("이벤트 드롭 후 Null 처리 끝 ");
    }
    public void Drop()
    {
        isMove = false;
        rigid.simulated = true;
        Debug.Log("드롭 발동");
        GameManager.Instance.lastUnitPrefab = null;
        //오브젝트 드롭 sfx 추가
    }





}






//  ***Study Notes***
// [TODO] 드롭한 후에는 이동 컨트롤 불가 로직 추가
//  - 특정 range 안에서만 이동 가능 -> rigidbody simulation 온오프로 해결! 
//  - 박스 range 안에 감지 후 이동 불가능
//  - Screen tap printer 에서 스타일러 사용 시 인풋설정 변화하는 법?


