using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit //������ ������
{
    //�ܺ� ���ٰ��� 
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
/* Index ����
 * EnemySOArray = EnemySO�� guys ���� []�� �ε���
 * Spawn Position Index = ��� ���� ����Ʈ vector3 ������Ʈ 
 * Enemy Type Index = Ȯ�������� ������� �����ε��� ��ȯ�� ���� ��� ��Ÿ��
 */