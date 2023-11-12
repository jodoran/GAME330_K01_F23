using System;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    /*  Input Keys 
     *  Event Publisher
     */

    //--------------------------------------
    //Key Inputs
    [NonSerialized] public string Akey = "Fire2";
    [NonSerialized] public string Bkey = "Fire1";
    [NonSerialized] public string Ykey = "Fire3";
    [NonSerialized] public string Xkey = "Jump";
    [NonSerialized] public string horizontal = "Horizontal";
    [NonSerialized] public string vertical = "Vertical";
    //---------------------------------------------
    //�̺�Ʈ �ڵ鷯
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
            if (OnAbuttonPressed == null) // ��� ���� null ��ȯ��
                Debug.Log("No subscribers for event");
        }
    }
}
