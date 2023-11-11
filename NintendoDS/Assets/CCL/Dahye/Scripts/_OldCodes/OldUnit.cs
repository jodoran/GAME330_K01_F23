using System;
using UnityEngine;

namespace oldcodes
{
    public class OldUnit : MonoBehaviour
    /* 이동과 드롭의 기능을 가진 유닛의 행동 스크립트
     * This script for : 
     * left & right movement behaviour
     * subscribe events <Drop>
    */

    {

        [SerializeField]
        private float speed = 3.0f;

        private bool canMove;
        private bool inBox;
        private bool isMerge;


        Rigidbody2D rigid;
        //Animator anim;


        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();

            canMove = true;
            inBox = false;
            rigid.simulated = false;
            //OnEnable(); // Event Subscription ON! 

            //anim = GetComponent<Animator>();
        }

        void Update()
        {
        }

        void FixedUpdate() //지속적인 키 입력은 FixedUpdate
        {
            horizontalMove();
        }

        void horizontalMove()
        {
            Debug.Log(inBox);
            if (canMove && !inBox)
            {
                var movement = Input.GetAxis(OldGameManager.Instance.horizontal);
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
        //----------충돌관련--------------------------------------------
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                // If the unit collides with a wall, stop its movement
                canMove = false;
            }
        }

        // Add a OnCollisionExit2D to handle when the unit stops colliding with walls
        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                canMove = true;     // If the unit stops colliding with a wall, allow movement again
            }
            if (collision.gameObject.tag == "Unit")
            {
                OldUnit other = collision.gameObject.GetComponent<OldUnit>();//유닛 두개 합치는 로직

            }
        }

        //-----------Event Subscriber-----------------------------------

        private void OnEnable()
        {
            OldGameManager.Instance.OnAbuttonPressed += HandleAbuttonPressed;
            Debug.Log("구독 온");
        }
        private void OnDisable() // A키 기능을 제거할 때 사용 
        {
            OldGameManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
            Debug.Log("구독 오프");
        }
        public void HandleAbuttonPressed(object sender, EventArgs e) // A 키 누르면 뭐 할건데?
        {
            // 드롭 하려면 오브젝트가 있어야하는데, null 이라면 return으로 나가고, 생성받고 다시와라.
            if (OldGameManager.Instance.lastUnitPrefab == null)
                return;
            Drop();

            Debug.Log("이벤트 드롭 후 Null 처리 끝");
        }
        public void Drop()
        {
            canMove = false; // 드롭된 오브젝트 좌우 컨트롤 불가
            inBox = true;
            rigid.simulated = true; // 드롭 
                                    //오브젝트 드롭 sfx 추가

            OldGameManager.Instance.lastUnitPrefab = null; // 드롭 됐으니 오브젝트 널 처리
            OnDisable(); // 드롭된 오브젝트 드롭기능 제거 
            Debug.Log(canMove);
        }

        //---------------------------------------------------------
    }
}

