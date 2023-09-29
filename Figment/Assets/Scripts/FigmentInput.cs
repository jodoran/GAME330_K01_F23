using UnityEngine;
using UnityEngine.UI;

public class FigmentInput : MonoBehaviour 
{
    //-----------버튼 종류 --------------------------------------------------------------
    // 주의사항 : int 형태로 사용
    public enum FigmentButton
    {
        LeftButton,     //0  좌
        RightButton,    //1  우
        ActionButton    //액션
    }
    //-----------이벤트 선언 "버튼이 ~~할 때"----------------------------------------------------
    public delegate void ButtonEvent(FigmentButton buttonType);

    public static event ButtonEvent OnButtonDown;    //버튼 눌렸을 때 
    public static event ButtonEvent OnButtonHold;    //버튼 눌려있는 동안
    public static event ButtonEvent OnButtonUp;     //버튼 뗐을 때

    //-----------버튼이 눌렸는지 아닌지 상태체크 T/F---------------------------------------------
    static bool[] FigmentButtonPressed;          //현재 프레임에 버튼이 눌렸는지
    static bool[] FigmentButtonPressedLastFrame; //지난 프레임에 버튼이 눌렸는지


    public Button[] buttons;


    //----------게임 시작과 동시에 버튼 초기화 : 현재버튼상태 추적을 위해--------------------------
    void Start () 
    {
        FigmentButtonPressed = new bool[System.Enum.GetValues(typeof(FigmentButton)).Length]; //Length 로 "Left,Right,Action" 세 개의 버튼 반환 
        FigmentButtonPressedLastFrame = new bool[System.Enum.GetValues(typeof(FigmentButton)).Length]; //Length 로 "Left,Right,Action" 세 개의 버튼 반환 
    }
    //-----------키보드 키 지정-----------------------------------------------------------------
    private KeyCode[] leftKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    private KeyCode[] rightKeys = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    private KeyCode[] actionKeys = new KeyCode[] { KeyCode.Space, KeyCode.Return, KeyCode.E };


    //-----------매 프레임 마다 키가 눌렸는지 확인 및 동작-----------------------------------------
    void Update () //마우스 사용관련 
    {
        // Only update if the mouse is not being used 
        if (!Input.GetMouseButton(0))
        {
            LaunchButtonEventsFromKeyboard(FigmentButton.LeftButton, leftKeys );
            LaunchButtonEventsFromKeyboard(FigmentButton.RightButton, rightKeys);
            LaunchButtonEventsFromKeyboard(FigmentButton.ActionButton, actionKeys);

            UpdateButtonStateFromKeyboard(FigmentButton.LeftButton, leftKeys);
            UpdateButtonStateFromKeyboard(FigmentButton.RightButton, rightKeys);
            UpdateButtonStateFromKeyboard(FigmentButton.ActionButton, actionKeys);
        }
    }

    public void PressButton(int buttonType) //인풋 버튼 눌렸는지 체크 & 이벤트를 실행(On Button Down, On Button Hold)
    {
        // Unfortunately have to use an int due to Unity's UI rules
        FigmentButtonPressed[buttonType] = true; 

        if (FigmentButtonPressedLastFrame[(int)buttonType] != true)
        {
            if (OnButtonDown != null)
            {
                OnButtonDown((FigmentButton)buttonType);
            }
        }

        if (OnButtonHold != null)
        {
            OnButtonHold((FigmentButton)buttonType);
        }
    }

    private void UpdateButtonStateFromKeyboard(FigmentButton buttonType, KeyCode[] keyboardKeys)
    {
        FigmentButtonPressedLastFrame[(int)buttonType] = FigmentButtonPressed[(int)buttonType]; //현재눌린 버튼을 과거눌린 버튼으로 넘김. 버튼 상태 저장

        foreach(KeyCode code in keyboardKeys) //지정한 키보드 키 눌린게 있는지 체크하는 함수
        {
            if(Input.GetKey(code))
            {
                FigmentButtonPressed[(int)buttonType] = true;
                return;
            }
        }
        FigmentButtonPressed[(int)buttonType] = false;
    } //키보드 키 눌렸는지 체크 및 상태 업데이트

    private void LaunchButtonEventsFromKeyboard(FigmentButton buttonType, KeyCode[] keyboardKeys)
    {
        foreach (KeyCode keyboardKey in keyboardKeys)
        {
            if (Input.GetKeyDown(keyboardKey))
            {
                if (OnButtonDown != null)
                {
                    OnButtonDown(buttonType);
                }

                Button button = buttons[(int)buttonType];
                button.image.color = button.colors.pressedColor;
            }

            if (Input.GetKey(keyboardKey))
            {
                if (OnButtonHold != null)
                {
                    OnButtonHold(buttonType);
                }

                Button button = buttons[(int)buttonType];
                button.image.color = button.colors.pressedColor;
            }

            if (Input.GetKeyUp(keyboardKey))
            {
                if (OnButtonUp != null)
                {
                    OnButtonUp(buttonType);
                }

                Button button = buttons[(int)buttonType];
                button.image.color = button.colors.normalColor;
            }
        }
    } //키 눌렸을 때 색상변경



    //-----------외부 호출할 버튼함수-----------------------------------------------------------------
    // 주의사항 : 

    /// <summary>
    /// Returns true while the button identified by buttonType is held down.
    /// </summary>
    /// <param name="buttonType">The button on the Figment</param>
    /// <returns>True when a button has been pressed and not released.</returns>
    public static bool GetButton(FigmentButton buttonType) //버튼을 인풋 하면
    {
        return FigmentButtonPressed[(int)buttonType]; //그의 맞는 행동을 아웃풋 
    }

    /// <summary>
    /// Returns true during the frame the user pressed down the button identified by buttonType.
    /// </summary>
    /// <param name="buttonType">The button on the Figment</param>
    /// <returns>True if the button was pressed down this frame, false if not</returns>
    public static bool GetButtonDown(FigmentButton buttonType)
    {
        return FigmentButtonPressed[(int)buttonType] && !FigmentButtonPressedLastFrame[(int)buttonType];
    }

    /// <summary>
    /// Returns true the first frame the user releases the button identified by buttonType.
    /// </summary>
    /// <param name="buttonType">The button on the Figment</param>
    /// <returns>True if the button was released this frame, false if not</returns>
    public static bool GetButtonUp(FigmentButton buttonType)
    {
        return !FigmentButtonPressed[(int)buttonType] && FigmentButtonPressedLastFrame[(int)buttonType];
    }
}
