using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }   
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject sound = new GameObject(sfxName + "Sound");
        AudioSource audioSource = sound.AddComponent<AudioSource>();
        audioSource.volume = 0.75f;
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(sound, clip.length); 
    }
}
