using Dev_Unit;
using UnityEngine;

public class Snowflake : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� Enemy ���̾ ���� ������
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Enemy���� �������� �ش�.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            // Snowflake �ı�
            Destroy(gameObject);
        }
    }
}
