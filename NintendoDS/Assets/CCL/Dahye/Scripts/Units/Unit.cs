using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Unit
/// </summary>
public class Unit : MonoBehaviour
{
    [Tooltip("Unit Control Speed")]
    private float speed = 4f;

    [Tooltip("Unit Falling Speed")]
    [SerializeField]
    private float dropSpeed = -1;

    [Tooltip("Explosion Layer")]
    [SerializeField]
    private LayerMask layerMaskHit;

    [Tooltip("Merge Explosion Area")]
    [SerializeField]
    private float impactField;

    [Tooltip("Merge Explosion Force")]
    [SerializeField]
    private float impactForce;

    //private Vector2 contactPos;

    private UnitLevel Level; // 유닛 레벨
    private new Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;

    private bool isMovable = false; // 움직일 수 있는가?

    /// <summary>
    /// 처음에는 움직일 수 없고, 시뮬레이션이 아닌 상태
    /// </summary>
    void Awake()
    {
        this.isMovable = false;
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.rigidbody.simulated = false;
        this.rigidbody.velocity = new Vector3(0, dropSpeed, 0);
        this.circleCollider = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// 초기 값으로 생성된 오브젝트가 유닛 타입과 머지로 생성된 타입인지 분류 및 저장
    /// </summary>
    /// <param name="UnitLevel">유닛 타입</param>
    /// <param name="isMerged"></param>
    public void Init(UnitLevel unitLevel, bool isMerged)
    {
        this.Level = unitLevel;
        this.isMovable = false;

        // 머지로 생성된 아이는 시뮬레이션을 하고, 움직일 수 없다.
        if (isMerged)
        {
            this.rigidbody.simulated = true;
            this.isMovable = false;
            Debug.Log("create merged Unit");
        }
        // 위에서 생긴 객체는 구독을 하며, 시뮬레이션이 아니며, 움직일 수 있다.
        else
        {
            InputManager.Instance.OnAbuttonPressed += dropping;
            this.rigidbody.simulated = false;
            this.isMovable = true;
            Debug.Log("create new unit");
        }
    }

    /// <summary>
    /// 정교한 Update()
    /// </summary>
    void FixedUpdate()
    {
        horizontalMove();
    }

    /// <summary>
    /// 키보드로 유닛을 움직인다.
    /// </summary>
    private void horizontalMove()
    {
        // 움직일수 없으면 리턴
        if (!this.isMovable)
            return;

        // axis를 받아서 그만큼 움직인다.
        var movement = Input.GetAxis(InputManager.Instance.horizontal);
        Vector3 newPosition = transform.position + new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        // Before moving, check if the new position would collide with a wall
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(movement, 0), Mathf.Abs(movement) * Time.deltaTime * speed);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // If a wall is hit, don't move
            return;
        }

        // 실제 움직임 작동 코드
        transform.position = newPosition;
    }

    /// <summary>
    /// A button을 누르면 input manager가 이 함수를 호출한다.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void dropping(object sender, EventArgs e) // 구독자
    {
        Debug.Log("dropping unit");
        // 호출은 한번만 이므로, 구독을 해제한다.
        InputManager.Instance.OnAbuttonPressed -= dropping;

        // 이제 유닛은 움직일 수 없으며, 시뮬레이션을 시작한다.
        this.isMovable = false;
        this.rigidbody.simulated = true;

        // A키를 눌럿다는 사실을 유닛 매니저에게 알려서 다음 유닛을 생성시킨다.
        UnitManager.Instance.DropComplete();
    }

    /// <summary>
    /// 충돌이 시작되면!
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision) // �浹 ��
    {
        // 이 아래 함수는 유닛과 유닛이 충돌했을 경우에만 실행하도록, 유닛 - 유닛 충돌이 아니라면 함수를 끝낸다.
        if (!collision.collider.CompareTag("Unit"))
            return;

        // 충돌한 객체의  유닛레벨 등의 정보 관리를 위해 컴포넌트를 가져온다.
        Unit otherUnit = collision.gameObject.GetComponent<Unit>();
        if (otherUnit == null)
        {
            Debug.Log("Collision null unit");
            return;
        }

        if (otherUnit.Level != this.Level)
            return;

        float meX = transform.position.x;
        float meY = transform.position.y;
        float otherX = otherUnit.transform.position.x;
        float otherY = otherUnit.transform.position.y;

        if (meY < otherY || (meY == otherY && meX > otherX))
        {
            // 충돌 포인트를 검색한다.
            Vector2 contactPos = collision.GetContact(0).point;

            // 자신과, 충돌한 객체를 숨긴다.
            this.Hide(otherUnit.transform.position);
            otherUnit.Hide(this.transform.position);

            // 충돌 지점 출력
            Debug.Log("collision point : " + contactPos);

            // 충돌했으면 다음 레벨을 시작한다.
            this.generateNextLevelUnit(contactPos);
        }
    }



    /// <summary>
    /// 객체를 숨기는 행위.
    /// </summary>
    /// <param name="targetPos"></param>
    public void Hide(Vector3 targetPos)
    {
        // 숨기는 객체는 시뮬레이션하지 않으며, 충돌도 일어나지 않습니다.
        this.rigidbody.simulated = false;
        this.circleCollider.enabled = false;

        StartCoroutine(HideRoutine(targetPos));

    }

    /// <summary>
    /// 객체를 숨길 때 행할 행동 : 두 객체를 점점 가까이 하다가 삭제합니다.
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;
        float mergeForce = 0.1f;

        // 가깝게 움직임
        while (frameCount < 3)
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, mergeForce);
            yield return null;
        }

        yield return new WaitForSeconds(0.01f);
        this.gameObject.SetActive(false);

        // 삭제
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 다음 레벨 유닛 생성 (머지로 인한 생성)
    /// </summary>
    /// <param name="contactPos"></param>
    private void generateNextLevelUnit(Vector2 contactPos)
    {
        Debug.Log("generate next level unit");
        // 움직이지 않도록 속도를 0으로 합니다.
        this.rigidbody.velocity = Vector2.zero;
        this.rigidbody.angularVelocity = 0;


        StartCoroutine(LevelUpRoutine(contactPos));
    }

    /// <summary>
    /// 다음 레벨 유닛 생성 (머지로 인한 생성, 코루틴)
    /// </summary>
    /// <param name="contactPos"></param>
    /// <returns></returns>
    IEnumerator LevelUpRoutine(Vector2 contactPos)
    {
        yield return new WaitForSeconds(0.005f);
        Explosion();
        UnitManager.Instance.MergeComplate(this.Level + 1, new Vector3(contactPos.x, contactPos.y, 0));
    }

    void Explosion()
    {
        Collider2D[] unitObjs = Physics2D.OverlapCircleAll(transform.position, impactField, layerMaskHit);
        foreach (Collider2D obj in unitObjs)
        {
            Vector2 dir = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(dir * impactForce);
            Debug.Log("Explode!" + impactForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }


}
