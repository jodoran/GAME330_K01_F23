using UnityEngine;


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
    [Tooltip("�� Ÿ��")]
    public EnemyType EnemyType;

    [Tooltip("�� ������")]
    public GameObject Prefeb;

    [Tooltip("�� ������ �ӵ�")]
    public float moveSpeed;

    [Tooltip("�� ü��")]
    public float hp;

    [Tooltip("�� ������")]
    public float damage;

    [Tooltip("�ִ� ���� ����")]
    public int maxCount;

    [Tooltip("�Ҹ�")]
    public AudioClip deadsfx;

    [Header("Wave Spawn Amount Probability Total : 100%")]
    public int wave1st = 0;
    public int wave2nd = 0;
    public int wave3rd = 0;

    //Sounds
}

/* Index ����
 * EnemySOArray = EnemySO�� guys ���� []�� �ε���
 * Spawn Position Index = ��� ���� ����Ʈ vector3 ������Ʈ 
 * Enemy Type Index = Ȯ�������� ������� �����ε��� ��ȯ�� ���� ��� ��Ÿ��
 */