using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class Enemy : EnemyBase
    {
        private int hitCount = 0; // Snowflake에 맞은 횟수
        public CameraShake cameraShake; // 카메라 흔들림 스크립트 참조

        protected override void TriggerEneterAbstract(Collider other)
        {
            // 추상 메서드 구현 내용
        }

        // 눈송이에 맞았을 때 호출되는 메서드
        public void TakeDamage(int damage)
        {
            hitCount += damage;

            // 카메라 흔들림 효과 적용
            if (cameraShake != null)
            {
                cameraShake.ShakeCamera();
            }

            // 세 번 맞으면 Enemy 파괴
            if (hitCount >= 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
