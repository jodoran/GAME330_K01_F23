using System;
using System.Collections;
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
    //public Vector3 contactPos;
    public float speed = 3.0f;

    private Rigidbody2D rigid;
    private CircleCollider2D circleCollider;


    private bool canMove;
    public bool CanMove; // ���ӿ��� �� �̵� ��Ȱ��ȭ

    private bool inBox;
    public bool InBox;   // ���ӿ��� ���ǿ� ���

    public bool IsMerge;


    //Animator anim;
    public UnitLevel Level;

    void Awake() // ���� �ʱ�ȭ
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        if (rigid == null)
        {
            Debug.LogError("Rigidbody2D ������Ʈ�� ã�� �� �����ϴ�.");
        }
        else
        {
            canMove = false;
            inBox = false;
            rigid.simulated = false;
            //anim = GetComponent<Animator>();
        }

    }
    void FixedUpdate() //�������� Ű �Է��� FixedUpdate
    {
        horizontalMove();
    }
    public void Init(bool isMergedObject)
    {
        if (isMergedObject)
        {
            canMove = false;
            rigid.simulated = true;
        }
        else
        {
            canMove = true;
            rigid.simulated = false;
        }
    }
    //--------------Move Ability----------------------------------

    public void horizontalMove()
    {
        if (canMove)
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
            return;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            Unit other = collision.gameObject.GetComponent<Unit>(); // other ���� ����

            if (this.Level == other.Level && !IsMerge && !other.IsMerge && this.Level < UnitLevel.Level11)
            {
                // ���� ����� ��ġ ��������
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                if (meY < otherY || (meY == otherY && meX > otherX))
                {   // �浹 �� ���� �ൿ
                    Vector2 contactPos = collision.GetContact(0).point;
                    // vfx ����
                    this.Hide(other.transform.position);
                    other.Hide(this.transform.position);


                    Debug.Log("collision point : " + contactPos);

                    this.LevelUp(contactPos);

                }
            }
        }

    }
    public void Hide(Vector3 targetPos)
    {
        IsMerge = true;

        rigid.simulated = false;
        circleCollider.enabled = false;

        StartCoroutine(HideRoutine(targetPos));

    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;
        float mergeForce = 0.005f;

        while (frameCount < 20)
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, mergeForce);
            yield return null;
        }

        IsMerge = false;
        gameObject.SetActive(false);
    }

    void LevelUp(Vector2 contactPos)
    {
        IsMerge = true;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine(contactPos));
    }

    IEnumerator LevelUpRoutine(Vector2 contactPos)
    {
        yield return new WaitForSeconds(0.1f);
        // Merge vfx 

        var nextLevelPrefab = UnitManager.Instance.LevelPrefab(this.Level + 1);

        var nextLevelUnit = Instantiate(nextLevelPrefab,
            new Vector3(contactPos.x, contactPos.y, 0),
            Quaternion.identity).GetComponent<Unit>();
        nextLevelUnit.Init(true);

        //yield return new WaitForSeconds(0.1f);

        IsMerge = false;

    }

    UnitLevel nextLevel(UnitLevel level)
    {
        return level + 1;
    }






    void OnCollisionExit2D(Collision2D collision) // �浹 ���� ��
    {
        if (collision.collider.CompareTag("Wall"))
        {
            canMove = true;     // If the unit stops colliding with a wall, allow movement again
        }
    }


}
