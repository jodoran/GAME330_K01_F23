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

    public void PlaySFX(AudioClip sfx)
    {
        EffectSound.PlayOneShot(sfx);
        EffectSound.Play();
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

}