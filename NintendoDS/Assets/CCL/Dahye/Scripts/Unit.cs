using System;
using UnityEngine;

public class Unit : MonoBehaviour
/* 이동과 드롭의 기능을 가진 유닛의 행동 스크립트
 * This script for : 
 * left & right movement behaviour
 * subscribe events <Drop>
*/

{

    [SerializeField]
    private float speed = 3.0f;

    private bool isMove;
    private bool inBox;


    Rigidbody2D rigid;
    //Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        isMove = true;
        inBox = false;
        rigid.simulated = false;
        //OnEnable(); // Event Subscription ON! 

        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Debug.Log(rigid.simulated);
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

        Debug.Log("이벤트 드롭 후 Null 처리 끝");
    }
    public void Drop()
    {
        isMove = false; // 드롭된 오브젝트 좌우 컨트롤 불가
        rigid.simulated = true; // 드롭 
        //오브젝트 드롭 sfx 추가

        GameManager.Instance.lastUnitPrefab = null; // 드롭 됐으니 오브젝트 널 처리
        OnDisable(); // 드롭된 오브젝트 드롭기능 제거 

    }

    //---------------------------------------------------------
}

