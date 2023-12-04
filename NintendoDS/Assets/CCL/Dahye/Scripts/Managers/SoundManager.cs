using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [Header("Audio Source")]
    [Tooltip("BGM Audio Source")]
    [SerializeField] private AudioSource MusicPlayer = null;

    [Tooltip("SFX Audio Source")]
    [SerializeField] private AudioSource EffectSound = null;

    [Space(10)]
    [Header("Audio Clip")]
    public AudioClip BGM;
    public AudioClip DropSfx;
    public AudioClip MergeSfx;

    float MusicVolume = 1f;
    float EffectVolume = 1f;

    protected override void Init()
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

    public void OffBGM()
    {
        //외부 audio source접근으로 off 하지 않고 내부 오프함수로 off 하는게 명확함. (참조+추적이 쉬움)
        MusicPlayer.Stop();
    }

    public void PlaySFX(AudioClip sfx)
    {
        EffectSound.PlayOneShot(sfx);
        EffectSound.Play();
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

    public void PauseBGM()
    {
        MusicPlayer.Pause();
    }

    public void ResumeBGM()
    {
        MusicPlayer.UnPause();
    }


}