using UnityEngine;


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
    [Tooltip("적 타입")]
    public EnemyType EnemyType;

    [Tooltip("적 프리팹")]
    public GameObject Prefeb;

    [Tooltip("적 움직임 속도")]
    public float moveSpeed;

    [Tooltip("적 체력")]
    public float hp;

    [Tooltip("적 데미지")]
    public float damage;

    [Tooltip("최대 스폰 개수")]
    public int maxCount;

    [Tooltip("소리")]
    public AudioClip deadsfx;

    [Header("Wave Spawn Amount Probability Total : 100%")]
    public int wave1st = 0;
    public int wave2nd = 0;
    public int wave3rd = 0;

    //Sounds
}

/* Index 놀이
 * EnemySOArray = EnemySO의 guys 들을 []로 인덱스
 * Spawn Position Index = 모든 스폰 포인트 vector3 오브젝트 
 * Enemy Type Index = 확률분포도 기반으로 랜덤인덱스 변환된 정보 담는 적타입
 */