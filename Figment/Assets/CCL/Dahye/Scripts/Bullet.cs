using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class Bullet : MonoBehaviour
    {
        public float bulletDamage = 25.0f;  // 플레이어 공격 데미지
        public bool bulletEnabled = false;  // Bullet 

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
                Debug.Log("에너미 감지!");
                if (collision != null)
                {
                    enemy.OnDamaged(bulletDamage);
                    Debug.Log("데미지 들어감");
                }
            }
        }
    }

}