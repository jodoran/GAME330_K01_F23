using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private static Sound instance = null;

    public static Sound I
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Sound)) as Sound;
                if (instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load("SoundManager")) as GameObject;
                    obj.name = "SoundManager";
                    instance = obj.GetComponent<Sound>();
                }
            }
            return instance;
        }
    }

    AudioSource _musicplayer = null;
    public AudioSource MusicPlayer
    {
        get
        {
            if (_musicplayer == null)
            {
                _musicplayer = Camera.main.GetComponent<AudioSource>();
            }
            return _musicplayer;
        }
    }

    //AudioSource _musicRun= null;
    //public AudioSource MusicRun
    //{
    //    get
    //    {
    //        if (_musicRun == null)
    //        {
    //            _musicRun = GetComponent<AudioSource>();
    //        }
    //        return _musicRun;
    //    }
    //}

    AudioSource _effectsound = null;
    public AudioSource EffectSound
    {
        get
        {
            if (_effectsound == null)
            {
                _effectsound = GetComponent<AudioSource>();
            }
            return _effectsound;
        }
    }

    float MusicVolume = 1f;
    float EffectVolume = 1f;

    private void Awake()
    {
        MusicVolume = 1f - PlayerPrefs.GetFloat("GameMusicVolume");
        EffectVolume = 1f - PlayerPrefs.GetFloat("GameEffectVolume");
        // 최초로 실행했을때 PlayerPrefs에 아무도 없어서 Volume은 0이됨 그래서 1을 뺀다
    }

    public void PlayBGM(AudioClip bgm, bool bLoop = true)
    {
        MusicPlayer.clip = bgm;
        MusicPlayer.loop = bLoop;
        MusicPlayer.Play();
    }

    public void PlayRun(AudioSource audioSource, bool bLoop)
    {
        audioSource.loop = bLoop;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }

    public void SetMusicVolume(float v)
    {
        MusicVolume = v;
        MusicPlayer.volume = v;
        // 미리 1을 빼놔서 저장할때는 1f - v로 다시 빼서 넣어야한다
        PlayerPrefs.SetFloat("GameMusicVolume", 1f - v);
    }

    public void SetEffectVolume(float v)
    {
        EffectVolume = v;
        EffectSound.volume = v;
        PlayerPrefs.SetFloat("GameEffectVolume", 1f - v);
    }

    public void PlayEffectSound(AudioClip eff, float pitch = 1.0f)
    {
        EffectSound.pitch = pitch;
        EffectSound.PlayOneShot(eff);
    }
    public void PlayEffectSound(AudioClip eff, AudioSource audioSource, float pitch = 1.0f)
    {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(eff);
    }

    public void PlayEffectVolume(GameObject obj, AudioClip eff)
    {
        AudioSource audio = obj.GetComponent<AudioSource>();
        if (audio == null)
        {
            audio = obj.AddComponent<AudioSource>();
        }
        audio.PlayOneShot(eff, EffectVolume);
    }
    // 프리팹은 안써도댐

}