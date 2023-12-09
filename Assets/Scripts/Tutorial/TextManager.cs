using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public TMP_Text tutorialTxt;

    string dialogue;

    public string[] tutorialDialogue;
    public string[] dialouges;

    public int talkNum;

    [Header("==========Assign==========")]
    public GameObject gm;
    public GameObject upScreen01;
    public GameObject downScreen01;
    public GameObject upScreen02;
    public GameObject upScreen03;
    public GameObject Text;
    //public GameObject Btn;
    //public float curTimer;
    //public float Timer;

    private void Awake()
    {
        //gm = GameObject.FindFirstObjectByType<GameManager>().gameObject;
        //gm = GameObject.Find("GameManager");
        //upScreen01 = GameObject.Find("01_UpScreen");
        //downScreen01 = GameObject.Find("01_DownScreen");
        //upScreen02 = GameObject.Find("02_UpScreen");
        //Text = GameObject.Find("01_DownText");
    }

    private void Start()
    {
        //dialogue = "";
        //StartCoroutine(Typing(dialogue));
        StartTalk(tutorialDialogue);
    }

    private void Update()
    {
        //curTimer = Timer + Time.deltaTime;
    }

    public void StartTalk(string[] talks)
    {
        dialouges = talks;

        StartCoroutine(Typing(dialouges[talkNum]));
    }

    public void nextTalk()
    {
        tutorialTxt.text = null;

        talkNum++;

        if (talkNum == dialouges.Length)
        {
            EndTalk();

            return;
        }

        StartCoroutine(Typing(dialouges[talkNum]));
    }

    public void EndTalk()
    {
        talkNum = 0;
        if (TutorialManager.instance.index == 0)
        {
            Text.SetActive(true);
            TutorialManager.instance.index++;
            //Btn.SetActive(true);
        }
        else if (TutorialManager.instance.index == 1)
        {
            upScreen01.SetActive(false);
            downScreen01.SetActive(false);
            Text.SetActive(false);
            gm.SetActive(true);
            TutorialManager.instance.tutorial = false;
        }
        else if (TutorialManager.instance.index == 2)
        {
            upScreen02.SetActive(false);
            downScreen01.SetActive(false);
            Text.SetActive(false);
            gm.SetActive(true);
            TutorialManager.instance.tutorial = false;
        }
        else if (TutorialManager.instance.index == 3)
        {
            upScreen03.SetActive(false);
            gm.SetActive(true);
            TutorialManager.instance.tutorial = false;
        }
        else if (TutorialManager.instance.index == 4)
        {
            TutorialManager.instance.NextBtn.SetActive(true);
            TutorialManager.instance.TempNextBtn.SetActive(false);
            TutorialManager.instance.upScreen04.SetActive(false);
        }
        Debug.Log("TalkDone");
    }

    IEnumerator Typing(string talk)
    {
        tutorialTxt.text = null;

        if (talk.Contains("  "))
        {
            talk = talk.Replace("  ", "\n");
        }

        for(int i = 0; i < talk.Length; i++)
        {
            tutorialTxt.text += talk[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(TutorialManager.instance.nextTalkDuration);;
        nextTalk();
    }
}
