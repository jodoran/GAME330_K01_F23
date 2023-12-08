using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public GameObject Select;
    public int index;

    private bool verticalInputProcessed = false;
    private bool downInputProcessed = false;
    private Animator selectanim;

    [Header("==========Audio=========")]
    public AudioClip clip;

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Select.activeSelf)
        {
            selectanim = Select.GetComponent<Animator>();

            float verticalInput = Input.GetAxisRaw("Vertical");

            if (verticalInput == 1f && !verticalInputProcessed) // UP
            {
                //Debug.Log("Up");
                if (index < 2)
                {
                    index++;
                    selectanim.SetTrigger("Up");
                    SoundManager.instance.SFXPlay("Btn", clip);
                }
                verticalInputProcessed = true;
            }
            else if (verticalInput == -1f && !downInputProcessed) // Down
            {
                //Debug.Log("Down");
                if (index > 0)
                {
                    index--;
                    selectanim.SetTrigger("Down");
                    SoundManager.instance.SFXPlay("Btn", clip);
                }
                downInputProcessed = true;
            }
            else if (verticalInput == 0f) // NotPressed
            {
                verticalInputProcessed = false;
                downInputProcessed = false;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //print("1");
            if (!Select.activeSelf)
            {
                //print("2");
                Select.SetActive(true);
                index = 2;
            }
            else
            {
                switch (index)
                {
                    case 2:
                        Debug.Log("Start");
                        Time.timeScale = 1;
                        SoundManager.instance.SFXPlay("Btn", clip);
                        Invoke(nameof(StarttheGame), 1f);
                        break;
                    case 1:
                        Debug.Log("Tutorial");
                        Time.timeScale = 1;
                        SoundManager.instance.SFXPlay("Btn", clip);
                        Invoke(nameof(Tutorial), 1f);
                        break;
                    case 0:
                        Debug.Log("Quit");
                        SoundManager.instance.SFXPlay("Btn", clip);
                        Application.Quit();
                        break;
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            SoundManager.instance.SFXPlay("Btn", clip);

            if (Select.activeSelf)
            {
                Select.SetActive(false);
            }
        }
    }

    public void StarttheGame()
    {
        SceneManager.LoadScene("NintendoDSTowerDefense");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
