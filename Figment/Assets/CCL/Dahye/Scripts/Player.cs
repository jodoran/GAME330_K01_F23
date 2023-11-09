using System.Collections;
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

    [SerializeField] private AudioClip shootsfx;

    private float lastDamageTime;

    //����
    private bool isBlinking = false;
    private SpriteRenderer playerRenderer;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        //����
        playerRenderer = GetComponent<SpriteRenderer>();

        // ������� ���� playerRenderer�� ����� �����Ǿ����� Ȯ���մϴ�.
        if (playerRenderer == null)
        {
            Debug.LogError("SpriteRenderer ������Ʈ�� Player ���� ������Ʈ�� �����ϴ�.");
        }
        else
        {
            Debug.Log("SpriteRenderer ������Ʈ�� ����� �����Ǿ����ϴ�.");
        }
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
    void OnCollisionEnter(Collision collision) // �浹 ��
    {
        if (collision.collider.CompareTag("Enemy"))
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

            SoundManager.Instance.PlayEffectSound(shootsfx);

        }

        // TODO: add sound
    }

    //����
    IEnumerator BlinkPlayer()
    {
        // �����Ÿ��� ���¸� �����մϴ�.
        isBlinking = true;

        // �����Ÿ��� ������ ������ �� �ð��� �����մϴ�. ���� �ð����� 4�ʸ� ���� ���Դϴ�.
        float endTime = Time.time + 4f;

        // ���� �ð��� �����Ÿ��� ������ ������ �� �ð����� ���� ���� �ݺ��մϴ�.
        while (Time.time < endTime)
        {
            // �÷��̾��� SpriteRenderer Ȱ�� ���¸� �������� �����Ÿ��� ȿ���� �ݴϴ�.
            playerRenderer.enabled = !playerRenderer.enabled;
            // 0.1�� ���� ����� �� ���� �ݺ��� �����մϴ�.
            yield return new WaitForSeconds(0.1f);
        }

        playerRenderer.enabled = true;
        //����
        isBlinking = false;
    }


}
