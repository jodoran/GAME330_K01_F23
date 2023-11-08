using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*  Input Keys 
     *  Event Publisher
     */

    //-------------�ܺ�����-----------------------

    private static InputManager _instance; // ����
    public static InputManager Instance // �ܺ�
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindObjectOfType<InputManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.Log("��ǲ�Ŵ��� �ν��Ͻ� �ߺ�! �����ϰڽ��ϴ�~");
            Destroy(gameObject);
        }
    }

    //--------------------------------------
    //Key Inputs
    public string Akey = "Fire2";
    public string Bkey = "Fire1";
    public string Ykey = "Fire3";
    public string Xkey = "Jump";
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";
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
            if (OnAbuttonPressed == null) // �̺�Ʈ�� NULL �̸� 
                Debug.Log("No subscribers for event");
        }
    }
}
