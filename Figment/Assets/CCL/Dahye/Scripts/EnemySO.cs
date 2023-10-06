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