using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float moveSpeed = 10.0f;
	
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
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
