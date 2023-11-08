using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*  Input Keys 
     *  Event Publisher
     */

    //-------------외부참조-----------------------

    private static InputManager _instance; // 내부
    public static InputManager Instance // 외부
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
            Debug.Log("인풋매니저 인스턴스 중복! 삭제하겠습니당~");
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
