using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /*  Unit Conditions         ^^
     *  Unit Movement           ^^
     *  Drop Event Subscriber   ^^
     *  Unit Collision Report
     *  Unit Destroy 
     *  
     *  Destroy animation
     *  Sound effects 
     */

    public float speed = 3.0f;

    private Rigidbody2D rigid;

    private bool canMove;
    public bool CanMove; // 게임오버 시 이동 비활성화

    private bool inBox;
    public bool InBox;   // 게임오버 조건에 사용

    public bool IsMerge;

    //Animator anim;


    void Awake() // 상태 초기화
    {
        rigid = GetComponent<Rigidbody2D>();

        if (rigid == null)
        {
            Debug.LogError("Rigidbody2D 컴포넌트를 찾을 수 없습니다.");
        }
        else
        {
            canMove = true;
            inBox = false;
            rigid.simulated = false;
            //anim = GetComponent<Animator>();
        }

    }

    void FixedUpdate() //지속적인 키 입력은 FixedUpdate
    {
        horizontalMove();
    }
    //--------------Move Ability----------------------------------

    public void horizontalMove()
    {
        if (canMove && !inBox)
        {
            var movement = Input.GetAxis(InputManager.Instance.horizontal);
            Vector3 newPosition = transform.position + new Vector3(movement, 0, 0) * Time.deltaTime * speed;

            // Before moving, check if the new position would collide with a wall
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(movement, 0), Mathf.Abs(movement) * Time.deltaTime * speed);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                // If a wall is hit, don't move
                return;
            }

            // If no wall is hit, proceed with the move
            transform.position = newPosition;
        }
    }

    //-----------Event Subscriber-----------------------------------

    private void OnEnable()
    {
        InputManager.Instance.OnAbuttonPressed += HandleAbuttonPressed;
        Debug.Log("구독 온");
    }

    public void HandleAbuttonPressed(object sender, EventArgs e) // A 키 누르면 뭐 할건데?
    {
        // 드롭 하려면 오브젝트가 있어야하는데, null 이라면 return으로 나가고, 생성받고 다시와라.
        if (UnitManager.Instance.lastUnitPrefab == null)
            return;
        Drop();

        Debug.Log("이벤트 드롭 후 Null 처리 끝");
    }

    public void Drop()
    {
        InputManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
        Debug.Log("구독 오프");

        canMove = false; // 드롭된 오브젝트 좌우 컨트롤 불가
        inBox = true;
        rigid.simulated = true; // 드롭 
                                //오브젝트 드롭 sfx 추가

        UnitManager.Instance.lastUnitPrefab = null; // 드롭 됐으니 오브젝트 널 처리
    }





    //----------충돌관련--------------------------------------------
    void OnCollisionEnter2D(Collision2D collision) // 충돌 시
    {
        if (collision.collider.CompareTag("Wall"))
        {
            canMove = false;    // If the unit collides with a wall, stop its movement
        }
        if (collision.gameObject.tag == "Unit")
        {
            Unit other = collision.gameObject.GetComponent<Unit>(); // other 유닛 정의

            //if (UnitManager.Instance.unitPrefabs == other.gameObject)
            //{ 같은 레벨 타입일 시 합치기
            //}

        }



    }

    void OnCollisionExit2D(Collision2D collision) // 충돌 해제 시
    {
        if (collision.collider.CompareTag("Wall"))
        {
            canMove = true;     // If the unit stops colliding with a wall, allow movement again
        }
    }


}
