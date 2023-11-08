using System;
using UnityEngine;

namespace oldcodes
{
    public class OldUnit : MonoBehaviour
    /* �̵��� ����� ����� ���� ������ �ൿ ��ũ��Ʈ
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

        void FixedUpdate() //�������� Ű �Է��� FixedUpdate
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
        //----------�浹����--------------------------------------------
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
                OldUnit other = collision.gameObject.GetComponent<OldUnit>();//���� �ΰ� ��ġ�� ����

            }
        }

        //-----------Event Subscriber-----------------------------------

        private void OnEnable()
        {
            OldGameManager.Instance.OnAbuttonPressed += HandleAbuttonPressed;
            Debug.Log("���� ��");
        }
        private void OnDisable() // AŰ ����� ������ �� ��� 
        {
            OldGameManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
            Debug.Log("���� ����");
        }
        public void HandleAbuttonPressed(object sender, EventArgs e) // A Ű ������ �� �Ұǵ�?
        {
            // ��� �Ϸ��� ������Ʈ�� �־���ϴµ�, null �̶�� return���� ������, �����ް� �ٽÿͶ�.
            if (OldGameManager.Instance.lastUnitPrefab == null)
                return;
            Drop();

            Debug.Log("�̺�Ʈ ��� �� Null ó�� ��");
        }
        public void Drop()
        {
            canMove = false; // ��ӵ� ������Ʈ �¿� ��Ʈ�� �Ұ�
            inBox = true;
            rigid.simulated = true; // ��� 
                                    //������Ʈ ��� sfx �߰�

            OldGameManager.Instance.lastUnitPrefab = null; // ��� ������ ������Ʈ �� ó��
            OnDisable(); // ��ӵ� ������Ʈ ��ӱ�� ���� 
            Debug.Log(canMove);
        }

        //---------------------------------------------------------
    }
}

