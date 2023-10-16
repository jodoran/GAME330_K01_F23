using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("�÷��̾� ������ �ӵ�")]
    public float MoveSpeed = 10.0f;

    [Tooltip("�Ѿ� Prefeb")]
    public GameObject BulletPrefeb;

    [Tooltip("���� �ð�")]
    public float NoDamageTime = 1.0f;

    [Tooltip("HP")]
    public float HP = 100;

    [Tooltip("�Ѿ� �߻� ��ġ")]
    public Transform snowPoint;

    private float lastDamageTime;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        Movement();
        Shoot();
    }

    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    public void Movement()
    {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    public void OnDamage(float damage)
    {
        Debug.Log("������ ����");
        var now = Time.time;
        if (now - lastDamageTime <= NoDamageTime)
        {
            Debug.Log("���� ����"); // ���� ���� ����Ʈ �߰� // ���� ���� bgm �߰�
            return;
        }

        lastDamageTime = now;
        HP -= damage;
        Debug.Log("Player HP : " + HP);
        //Camera.Instance.Shake(); // ī�޶� ����ũ ��� �߰�
        if (HP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// �÷��̾� �Ѿ� ��� 
    /// </summary>
    public void Shoot() //ä��
    {
        if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) //�ǵ� ��� //GetButton Down/Up
        {
            GameObject bullet = Instantiate(BulletPrefeb, snowPoint.position, snowPoint.rotation);
            bullet.GetComponent<Bullet>().Shoot(snowPoint); // Bullet ��ũ��Ʈ�� Shoot �Լ� ȣ��
        }

        // TODO: add sound
    }
}
