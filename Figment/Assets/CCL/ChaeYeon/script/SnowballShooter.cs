using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    public GameObject snowballPrefab; // ������ ������
    public Transform firePoint; // �߻� ��ġ

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // ���콺 ���� ��ư�̳� ���̽�ƽ ��ư�� ������ ��
        {
            Shoot();
        }
    }

   public Shoot()
    {
        GameObject snowball = Instantiate(snowballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = snowball.GetComponent<Rigidbody>();

        float forceAmount = 30f; // �� ���� �����Ͽ� �߻� �ӵ��� ������ �� �ֽ��ϴ�.

        rb.AddForce(firePoint.forward * forceAmount, ForceMode.Impulse);
    }

}
