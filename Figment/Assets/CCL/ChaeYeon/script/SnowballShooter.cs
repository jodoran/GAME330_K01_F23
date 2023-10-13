using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    public GameObject snowballPrefab; // 눈송이 프리팹
    public Transform firePoint; // 발사 위치

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // 마우스 왼쪽 버튼이나 조이스틱 버튼을 눌렀을 때
        {
            Shoot();
        }
    }

   public Shoot()
    {
        GameObject snowball = Instantiate(snowballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = snowball.GetComponent<Rigidbody>();

        float forceAmount = 30f; // 이 값을 조정하여 발사 속도를 변경할 수 있습니다.

        rb.AddForce(firePoint.forward * forceAmount, ForceMode.Impulse);
    }

}
