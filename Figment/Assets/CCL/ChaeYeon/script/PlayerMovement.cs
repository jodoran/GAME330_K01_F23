using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float moveSpeed = 10.0f;
    public ParticleSystem snowflakeShooter; // 파티클 시스템 참조

    void Update () 
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
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.ActionButton)) //건들 노노 //GetButton Down/Up
        {
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //액션버튼 누르면 플레이어 전진하는거 
            
            Debug.Log("Shooting!"); // 로그가 출력되는지 확인

            snowflakeShooter.Play(); // 파티클 시스템 활성화
        }
    }
}
