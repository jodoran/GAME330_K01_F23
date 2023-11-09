using System;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    /*  Input Keys 
     *  Event Publisher
     */

    //--------------------------------------
    //Key Inputs
    public string Akey = "Fire2";
    public string Bkey = "Fire1";
    public string Ykey = "Fire3";
    public string Xkey = "Jump";
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";
    //---------------------------------------------
    //이벤트 핸들러
    public event EventHandler OnAbuttonPressed;

    void Update()
    {
        AButtonPressed();
    }



    public void AButtonPressed()
    {
        if (Input.GetButtonDown(Akey))
        {
            OnAbuttonPressed?.Invoke(this, EventArgs.Empty);
            if (OnAbuttonPressed == null) // 이벤트가 NULL 이면 
                Debug.Log("No subscribers for event");
        }
    }
}
