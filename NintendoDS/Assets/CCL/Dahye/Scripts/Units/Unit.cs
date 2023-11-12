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
    public bool CanMove; // ���ӿ��� �� �̵� ��Ȱ��ȭ

    private bool inBox;
    public bool InBox;   // ���ӿ��� ���ǿ� ���

    public bool IsMerge;

    //Animator anim;


    void Awake() // ���� �ʱ�ȭ
    {
        rigid = GetComponent<Rigidbody2D>();

        if (rigid == null)
        {
            Debug.LogError("Rigidbody2D ������Ʈ�� ã�� �� �����ϴ�.");
        }
        else
        {
            canMove = true;
            inBox = false;
            rigid.simulated = false;
            //anim = GetComponent<Animator>();
        }

    }

    void FixedUpdate() //�������� Ű �Է��� FixedUpdate
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
        Debug.Log("���� ��");
    }

    public void HandleAbuttonPressed(object sender, EventArgs e) // A Ű ������ �� �Ұǵ�?
    {
        // ��� �Ϸ��� ������Ʈ�� �־���ϴµ�, null �̶�� return���� ������, �����ް� �ٽÿͶ�.
        if (UnitManager.Instance.lastUnitPrefab == null)
            return;
        Drop();

        Debug.Log("�̺�Ʈ ��� �� Null ó�� ��");
    }

    public void Drop()
    {
        InputManager.Instance.OnAbuttonPressed -= HandleAbuttonPressed;
        Debug.Log("���� ����");

        canMove = false; // ��ӵ� ������Ʈ �¿� ��Ʈ�� �Ұ�
        inBox = true;
        rigid.simulated = true; // ��� 
                                //������Ʈ ��� sfx �߰�

        UnitManager.Instance.lastUnitPrefab = null; // ��� ������ ������Ʈ �� ó��
    }





    //----------�浹����--------------------------------------------
    void OnCollisionEnter2D(Collision2D collision) // �浹 ��
    {
        if (collision.collider.CompareTag("Wall"))
        {
            canMove = false;    // If the unit collides with a wall, stop its movement
        }
        if (collision.gameObject.tag == "Unit")
        {
            Unit other = collision.gameObject.GetComponent<Unit>(); // other ���� ����

            //if (UnitManager.Instance.unitPrefabs == other.gameObject)
            //{ ���� ���� Ÿ���� �� ��ġ��
            //}

        }



    }

    void OnCollisionExit2D(Collision2D collision) // �浹 ���� ��
    {
        if (collision.collider.CompareTag("Wall"))
        {
            canMove = true;     // If the unit stops colliding with a wall, allow movement again
        }
    }


}
