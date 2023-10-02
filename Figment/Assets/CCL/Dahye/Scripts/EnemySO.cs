using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemySO : ScriptableObject
{

    public enum EnemyType
    {
        BigGuy,
        SmallGuy,
        ShootingGuy
    }

    public EnemyType enemyType;
    public GameObject EnemyPrefab;
    public float moveSpeed;
    public float hp;
    public float damage;
    public bool isDead;


}
