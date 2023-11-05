using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;


    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
                if (instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load("SoundManager")) as GameObject;
                    obj.name = "SoundManager";
                    instance = obj.GetComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [Tooltip("BGM Audio Source")]
    [SerializeField] private AudioSource MusicPlayer = null;
    [Tooltip("SFX Audio Source")]
    [SerializeField] private AudioSource EffectSound = null;

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

    public void OffBGM()
    {
        //외부 audio source접근으로 off 하지 않고 내부 오프함수로 off 하는게 명확함. (참조+추적이 쉬움)
        MusicPlayer.Stop();
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