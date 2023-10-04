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

  

        public EnemyType enemyType;
        public GameObject enemyPrefab;
        //public Vector3 position;
        public float moveSpeed;
        public float hp;
        public float damage;
        public bool isDead;


    }

}