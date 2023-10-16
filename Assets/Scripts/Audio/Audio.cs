using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum AudioType
//{
//    Btn,
//    Hit,
//    Coin,
//    Extra,
//    Lose,
//    Win
//}

public class Audio : MonoBehaviour
{
    //public static Action audio;

    //public AudioType currentType;

    public AudioSource audioSource;
    public AudioClip Btn;
    public AudioClip Hit;
    public AudioClip Coin;
    public AudioClip Extra;
    public AudioClip Lose;
    public AudioClip Win;

    //public void Awake()
    //{
    //    audio = () =>
    //    {
    //        AudioBtn();
    //        AudioHit();
    //        AudioCoin();
    //        AudioExtra();
    //        AudioLose();
    //        AudioWin();
    //    };
    //}

    public void AudioBtn()
    {
        audioSource.clip = Btn;
        audioSource.Play();
    }

    public void AudioHit()
    {
        audioSource.clip = Hit;
        audioSource.Play();
    }

    public void AudioCoin()
    {
        audioSource.clip = Coin;
        audioSource.Play();
    }

    public void AudioExtra()
    {
        audioSource.clip = Extra;
        audioSource.Play();
    }

    public void AudioLose()
    {
        audioSource.clip = Lose;
        audioSource.Play();
    }

    public void AudioWin()
    {
        audioSource.clip = Win;
        audioSource.Play();
    }
}
