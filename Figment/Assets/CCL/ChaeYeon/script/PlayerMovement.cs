using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    // public ParticleSystem snowflakeShooter; // 파티클 시스템 (없앰)

    public GameObject snowballPrefab; // 눈송이 프리팹
    public Transform snowPoint; // 발사 위치
    public float snowSpeed = 10;

    AudioSource snowSource;
    public AudioClip snowSfx;

    private void Start()
    {
        snowSource = GetComponent<AudioSource>();

    }


    void Update()
    {
        Movement();
        Shoot();

    }

    public void Movement()
    {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void Shoot() //채연
    {
        if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) //건들 노노 //GetButton Down/Up
        {


            // 스노우볼 인스턴스 생성
            GameObject snowball = Instantiate(snowballPrefab, snowPoint.position, snowPoint.rotation);

            // 스노우볼에 Rigidbody 컴포넌트가 있다면, 힘을 추가하여 발사
            Rigidbody rb = snowball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(snowPoint.forward * snowSpeed, ForceMode.Impulse);
            }

            //snowSource.PlayOneShot(snowSfx);
            Destroy(snowball, 10f);
        }

    }
}
