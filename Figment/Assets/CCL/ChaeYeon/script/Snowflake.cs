using UnityEngine;

public class Snowflake : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Enemy 레이어에 속해 있으면
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Enemy에게 데미지를 준다.
            Enemy_DH enemy = collision.gameObject.GetComponent<Enemy_DH>();
            if (enemy != null)
            {
                //enemy.TakeDamage(1); Take Damage 함수가 있어?? -다혜
            }

            // Snowflake 파괴
            Destroy(gameObject);
        }
    }
}
