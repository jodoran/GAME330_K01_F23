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

    public string EnemyName;
    public Transform EnemyPrefab;
    public float moveSpeed;
    public float hp;
    public float damage;


}
