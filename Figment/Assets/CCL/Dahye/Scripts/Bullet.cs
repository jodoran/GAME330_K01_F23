using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class Bullet : MonoBehaviour
    {
        public float bulletDamage = 25.0f;  // �÷��̾� ���� ������
        public bool bulletEnabled = false;  // Bullet 

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
                Debug.Log("���ʹ� ����!");
                if (collision != null)
                {
                    enemy.OnDamaged(bulletDamage);
                    Debug.Log("������ ��");
                }
            }
        }
    }

}