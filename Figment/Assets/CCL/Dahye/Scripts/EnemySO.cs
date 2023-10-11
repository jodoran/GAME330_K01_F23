using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit //관리가 수월함
{
    //외부 접근가능 
    public enum EnemyType
    {
        BigGuy,
        SmallGuy,
        ShootingGuy
    }

    [CreateAssetMenu]
    public class EnemySO : ScriptableObject
    {
        //Enemy
        public EnemyType enemyType;
        public GameObject enemyPrefab;

        //public Vector3 position;
        public float moveSpeed;
        public float hp;
        public float damage;
        public bool isDead;

        //Spawn Info
        public int maxCount;

        [Header("Wave Spawn Amount Probability Total : 100%")] 
        public int wave1st = 0;
        public int wave2nd = 0;
        public int wave3rd = 0;



    }

}
/* Index 놀이
 * EnemySOArray = EnemySO의 guys 들을 []로 인덱스
 * Spawn Position Index = 모든 스폰 포인트 vector3 오브젝트 
 * Enemy Type Index = 확률분포도 기반으로 랜덤인덱스 변환된 정보 담는 적타입
 */