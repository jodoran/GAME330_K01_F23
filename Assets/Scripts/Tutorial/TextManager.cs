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
    public float nextTalkDuration;

    [Header("==========Assign==========")]
    public GameObject gm;
    public GameObject upScreen01;
    public GameObject downScreen01;
    public GameObject Text;
    //public GameObject Btn;

    private void Start()
    {
        //dialogue = "";
        //StartCoroutine(Typing(dialogue));
        StartTalk(tutorialDialogue);
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
            gm.SetActive(true);
            TutorialManager.instance.tutorial = false;
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

        yield return new WaitForSeconds(nextTalkDuration);;
        nextTalk();
    }
}
