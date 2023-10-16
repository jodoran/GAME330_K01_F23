using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("플레이어 움직임 속도")]
    public float MoveSpeed = 10.0f;

    [Tooltip("총알 Prefeb")]
    public GameObject BulletPrefeb;

    [Tooltip("무적 시간")]
    public float NoDamageTime = 1.0f;

    [Tooltip("HP")]
    public float HP = 100;

    [Tooltip("총알 발사 위치")]
    public Transform snowPoint;

    private float lastDamageTime;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        Movement();
        Shoot();
    }

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    public void Movement()
    {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// 플레이어 데미지 받음
    /// </summary>
    public void OnDamage(float damage)
    {
        Debug.Log("데미지 받음");
        var now = Time.time;
        if (now - lastDamageTime <= NoDamageTime)
        {
            Debug.Log("무적 상태"); // 무적 상태 이펙트 추가 // 무적 상태 bgm 추가
            return;
        }

        lastDamageTime = now;
        HP -= damage;
        Debug.Log("Player HP : " + HP);
        //Camera.Instance.Shake(); // 카메라 쉐이크 기능 추가
        if (HP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// 플레이어 총알 쏘기 
    /// </summary>
    public void Shoot() //채연
    {
        if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) //건들 노노 //GetButton Down/Up
        {
            GameObject bullet = Instantiate(BulletPrefeb, snowPoint.position, snowPoint.rotation);
            bullet.GetComponent<Bullet>().Shoot(snowPoint); // Bullet 스크립트의 Shoot 함수 호출
        }

        // TODO: add sound
    }
}
