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

    UnitLevel Level; // 유닛 레벨
    new Rigidbody2D rigidbody;
    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;

    public bool isMerged;


    private bool isMovable = false;
    private float deadTime;
    /// <summary>
    /// 처음에는 움직일 수 없고, 시뮬레이션이 아닌 상태
    /// </summary>
    void Awake()
    {
        this.isMovable = false;
        this.isMerged = false;
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.rigidbody.simulated = false;
        this.rigidbody.velocity = new Vector3(0, dropSpeed, 0);
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
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

    void FixedUpdate()
    {
        horizontalMove();
    }


    private void horizontalMove()
    {
        // 움직일수 없으면 리턴
        if (!this.isMovable)
            return;
        if (GameManager.Instance.IsGameOver)
            return;

        // axis를 받아서 그만큼 움직인다.
        var movement = Input.GetAxis(InputManager.Instance.horizontal);
        Vector3 newPosition = transform.position + new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(movement, 0), Mathf.Abs(movement) * Time.deltaTime * speed);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            return;
        }

        // 실제 움직임 작동 코드
        transform.position = newPosition;
    }
    private void dropping(object sender, EventArgs e) // 구독자
    {
        if (GameManager.Instance.IsGameOver)
            return;
        // 호출은 한번만 이므로, 구독을 해제한다.
        InputManager.Instance.OnAbuttonPressed -= dropping;

        // 이제 유닛은 움직일 수 없으며, 시뮬레이션을 시작한다.
        this.isMovable = false;
        this.rigidbody.simulated = true;

        // A키를 눌럿다는 사실을 유닛 매니저에게 알려서 다음 유닛을 생성시킨다.
        UnitManager.Instance.DropComplete();
        SoundManager.Instance.PlaySFX(SoundManager.Instance.DropSfx);
    }

    void OnCollisionEnter2D(Collision2D collision)
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

        if (!isMerged && !otherUnit.isMerged && (int)Level < 10)
        {
            if (meY < otherY || (meY == otherY && meX > otherX))
            {
                // 충돌 포인트를 검색한다.
                Vector2 contactPos = collision.GetContact(0).point;
                Explosion();

                // 자신과, 충돌한 객체를 숨긴다.
                this.Hide(otherUnit.transform.position);
                otherUnit.Hide(this.transform.position);

                // 충돌 지점 출력
                Debug.Log("collision point : " + contactPos);

                // 충돌했으면 다음 레벨을 시작한다.
                this.generateNextLevelUnit(contactPos);

            }

        }

    }

    public void Hide(Vector3 targetPos)
    {
        isMerged = true;
        // 숨기는 객체는 시뮬레이션하지 않으며, 충돌도 일어나지 않습니다.
        this.rigidbody.simulated = false;
        this.circleCollider.enabled = false;

        StartCoroutine(HideRoutine(targetPos));

    }
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


    private void generateNextLevelUnit(Vector2 contactPos)
    {
        Debug.Log("generate next level unit");
        // 움직이지 않도록 속도를 0으로 합니다.
        this.rigidbody.velocity = Vector2.zero;
        this.rigidbody.angularVelocity = 0;


        StartCoroutine(LevelUpRoutine(contactPos));
    }

    IEnumerator LevelUpRoutine(Vector2 contactPos)
    {
        yield return new WaitForSeconds(0.005f);
        UnitManager.Instance.maxLevel = Mathf.Max((int)Level, UnitManager.Instance.maxLevel);
        UnitManager.Instance.MergeComplate(this.Level + 1, new Vector3(contactPos.x, contactPos.y, 0));
        SoundManager.Instance.PlaySFX(SoundManager.Instance.MergeSfx);
    }

    void Explosion()
    {
        Collider2D[] unitObjs = Physics2D.OverlapCircleAll(transform.position, impactField, layerMaskHit);
        foreach (Collider2D obj in unitObjs)
        {
            Vector2 dir = obj.transform.position - transform.position;
            //var force = dir * impactForce * 100;
            //GetComponent<SpriteRenderer>().color = Color.red;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * impactForce * 5);
            Debug.Log("Explode!" + impactForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Debug.Log("Line Touched");
            deadTime += Time.deltaTime;
            if (deadTime > .5)
            {
                spriteRenderer.color = Color.red;
            }
            if (deadTime > 1)
            {
                //this.gameObject.SetActive(false);
                Destroy(gameObject);
                GameManager.Instance.GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            deadTime = 0;
            spriteRenderer.color = Color.white;
        }
    }


}
