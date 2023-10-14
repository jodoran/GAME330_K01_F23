using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class Enemy : EnemyBase
    {
        private int hitCount = 0; // Snowflake�� ���� Ƚ��
        public CameraShake cameraShake; // ī�޶� ��鸲 ��ũ��Ʈ ����

        protected override void TriggerEneterAbstract(Collider other)
        {
            // �߻� �޼��� ���� ����
        }

        // �����̿� �¾��� �� ȣ��Ǵ� �޼���
        public void TakeDamage(int damage)
        {
            hitCount += damage;

            // ī�޶� ��鸲 ȿ�� ����
            if (cameraShake != null)
            {
                cameraShake.ShakeCamera();
            }

            // �� �� ������ Enemy �ı�
            if (hitCount >= 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
