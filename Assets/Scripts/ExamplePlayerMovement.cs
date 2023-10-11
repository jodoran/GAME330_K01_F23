using UnityEngine;

public class ExamplePlayerMovement : MonoBehaviour 
{
    public float turnSpeed = 120.0f;
    public float moveSpeed = 8.0f;
	
	// Update is called once per frame
	void Update () 
    {
        // Rotate the player by pressing left or right
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        // If we press the action button, move forward
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.ActionButton))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
