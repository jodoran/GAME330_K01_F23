using UnityEngine;
using UnityEngine.Events;

public class Enemy_DH : MonoBehaviour
{
    private enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    public UnityAction OnDead;

    private State state;
    private float hp;
    private float moveSpeed = 2.0f;
    private float attackCoolTime = 1.0f;
    private float currTime = 0f;
    private float lastAttackTime = 0;
    private float damage = 1;
    private Animator animator;
    private Collider triggerCollider;
    private Transform target;

    [Space(5.0f), Header("SFX")]
    [SerializeField] private AudioClip deadsfx;
    [SerializeField] private AudioClip attacksfx;

    [Space(5.0f), Header("VFX")]
    [SerializeField] private ParticleSystem spawnvfx;
    [SerializeField] private ParticleSystem deadvfx;
    [SerializeField] private ParticleSystem attackvfx;


    // 에너미 데미지량 == 플레이어가 에너미 죽일 시 얻는 스코어 량
    public int Score { get { return (int)damage; } }

    /// <summary>
    /// 생성자, Enemy 수치값 초기셋팅
    /// </summary>
    public void Setting(EnemySO enemySO)
    {
        this.moveSpeed = enemySO.moveSpeed;
        this.hp = enemySO.hp;
        this.state = State.Idle;
        this.damage = enemySO.damage;
        this.deadsfx = enemySO.deadsfx;
        spawnvfx.Play();

        UnitManager.Instance.AddUnit(this);
    }

    /// <summary>
    /// 애니메이터를 저장합니다.
    /// </summary>
    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 플레이어의 위치를 찾아 타겟으로 지정합니다.
    /// </summary>
    void Start()
    {
        this.target = FindObjectOfType<Player>().transform;
        changeState(State.Move);
    }

    /// <summary>
    /// 일정량의 데미지를 받고, hp가 모두 없어지면 Die()를 호출합니다.
    /// </summary>
    public void OnDamaged(float damage)
    {
        this.hp -= damage;
        Debug.Log("데미지 량 :" + damage + "현재 체력 : " + hp);
        if (hp <= 0f)
            changeState(State.Dead);
    }

    /// <summary>
    /// 에너미의 상태를 변경합니다.
    /// </summary>
    private void changeState(State state)
    {
        if (state != this.state)
        {
            // Debug.Log("enemy state 변경 : " + this.state + " -> " + state);
            this.state = state;
        }

        switch (this.state)
        {
            case State.Dead:
                {
                    UnitManager.Instance.RemoveUnit(this);
                    SoundManager.Instance.PlayEffectSound(this.deadsfx);
                    deadvfx?.Play();

                    Destroy(gameObject, 2.0f); // 즉시 죽음
                    return;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 에너미가 벽 또는 플레이어에 충돌하면 멈추고 공격 상태로 진입합니다.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall"))
        {
            changeState(State.Attack);
            triggerCollider = other;
        }
    }

    /// <summary>
    /// 에너미가 아무것도 안닿아 있으면 움직입니다.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other == null)
            return;

        changeState(State.Move);
        state = State.Move;
    }

    private void Update()
    {
        switch (state)
        {
            // 움직임
            case State.Move:
                {
                    if (target == null)
                    {
                        Debug.Log("타켓 미 감지");
                        return;
                    }

                    var direction = transform.position - target.position;
                    if (direction.magnitude > 1.0f)
                    {
                        var move = direction.normalized;
                        move.y = 0;
                        transform.Translate(move * moveSpeed * Time.deltaTime);
                    }

                    return;
                }

            // 공격
            case State.Attack:
                {
                    // 공격할 대상이 없어졋으면 다시 이동
                    if (this.triggerCollider == null)
                    {
                        changeState(State.Move);
                        return;
                    }

                    // 공격할 대상이 있으면 일정 시간 간격으로 어택
                    var now = Time.time;
                    currTime += Time.deltaTime;

                    if (currTime > attackCoolTime)
                    {
                        currTime = 0;

                        if (this.triggerCollider.CompareTag("Player"))
                        {
                            var player = this.triggerCollider.GetComponent<Player>();
                            player.OnDamage(this.damage);
                            Debug.Log("적 : 플레이어 어택!");

                            SoundManager.Instance.PlayEffectSound(attacksfx);
                            attackvfx?.Play();
                        }
                        else if (this.triggerCollider.CompareTag("Wall"))
                        {
                            var wall = this.triggerCollider.GetComponent<Wall>();
                            wall.OnDamage(this.damage);

                            SoundManager.Instance.PlayEffectSound(attacksfx);
                            attackvfx?.Play();
                        }
                        this.lastAttackTime = now;
                    }

                    //if (now - this.lastAttackTime > this.attackCoolTime)
                    //{


                    //}

                    return;
                }


        }
    }
}