using UnityEngine;

public class Snowflake : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� Enemy ���̾ ���� ������
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Enemy���� �������� �ش�.
            Enemy_DH enemy = collision.gameObject.GetComponent<Enemy_DH>();
            if (enemy != null)
            {
                //enemy.TakeDamage(1); Take Damage �Լ��� �־�?? -����
            }

            // Snowflake �ı�
            Destroy(gameObject);
        }
    }
}
