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
        // ���ʷ� ���������� PlayerPrefs�� �ƹ��� ��� Volume�� 0�̵� �׷��� 1�� ����
    }

    public void PlayBGM(AudioClip bgm, bool bLoop = true)
    {
        MusicPlayer.clip = bgm;
        MusicPlayer.loop = bLoop;
        MusicPlayer.Play();
    }

    public void OffBGM()
    {
        //�ܺ� audio source�������� off ���� �ʰ� ���� �����Լ��� off �ϴ°� ��Ȯ��. (����+������ ����)
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
        // �̸� 1�� ������ �����Ҷ��� 1f - v�� �ٽ� ���� �־���Ѵ�
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
    // �������� �Ƚᵵ��

}