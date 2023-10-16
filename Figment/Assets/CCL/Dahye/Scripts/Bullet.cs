using UnityEngine;

// �Ѿ�
public class Bullet : MonoBehaviour
{
    [Tooltip("�Ѿ��� �ִ� ������")]
    [SerializeField] private float damage = 25.0f;

    [Tooltip("�Ѿ��� �ִ��� ���󰡴� �ð�")]
    [SerializeField] private float ttl = 5.0f; // time to live 

    [Tooltip("�Ѿ� �ӵ�")]
    public float speed = 10;

    /// <summary>
    /// �Ѿ��� �������� ����, Shoot()�� ȣ���ϸ� �� �� �ִ�.
    /// </summary>
    private bool isEnable = false;

    /// <summary>
    /// �Ѿ��� ���󰡴� ����
    /// </summary>
    private Vector3 direction;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    /// <summary>
    /// �Ѿ��� �����̰� ����ϴ�.
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
    /// �Ѿ��� ��򰡿� �ε��� �� ȣ��.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // �Ѿ��� ���� ���� ����
        if (!isEnable)
            return;

        // �浹�̻� üũ
        if (collision == null)
            return;

        // ���� �浹 ���ϸ� //early return
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        // ���ʹ� �ҷ�����
        Enemy_DH enemy = collision.gameObject.GetComponent<Enemy_DH>();
        if (enemy != null)
        {
            // ������ ������
            enemy.OnDamaged(this.damage);
            Debug.Log("������ ��");

            // Bullet �ı�
            Destroy(gameObject);
        }
    }
}