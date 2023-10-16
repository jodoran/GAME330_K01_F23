using UnityEngine;

// 총알
public class Bullet : MonoBehaviour
{
    [Tooltip("총알이 주는 데미지")]
    [SerializeField] private float damage = 25.0f;

    [Tooltip("총알이 최대한 날라가는 시간")]
    [SerializeField] private float ttl = 5.0f; // time to live 

    [Tooltip("총알 속도")]
    public float speed = 10;

    /// <summary>
    /// 총알이 쏴졋는지 여부, Shoot()을 호출하면 쏠 수 있다.
    /// </summary>
    private bool isEnable = false;

    /// <summary>
    /// 총알이 날라가는 방향
    /// </summary>
    private Vector3 direction;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    /// <summary>
    /// 총알을 움직이게 만듭니다.
    /// </summary>
    public void Shoot(Transform transform)
    {
        this.isEnable = true;

        var rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        Destroy(gameObject, ttl);
    }

    /// <summary>
    /// 총알이 어딘가에 부딪힐 때 호출.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // 총알이 아직 쏘지 않음
        if (!isEnable)
            return;

        // 충돌이상 체크
        if (collision == null)
            return;

        // 적과 충돌 안하면 //early return
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        // 에너미 불러오기
        Enemy_DH enemy = collision.gameObject.GetComponent<Enemy_DH>();
        if (enemy != null)
        {
            // 적한테 데미지
            enemy.OnDamaged(this.damage);
            Debug.Log("데미지 들어감");

            // Bullet 파괴
            Destroy(gameObject);
        }
    }
}