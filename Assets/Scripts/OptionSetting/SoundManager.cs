using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource myBGM;
    float absoluteBGMSound;
    float relativeVol;
    public float bgmVolume
    {
        get => absoluteBGMSound;
        set 
        {
            absoluteBGMSound = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("BGM_VOLUME", absoluteBGMSound);
        }
    }
    float _effect = 0f;
    public float effectVolume
    {
        get => _effect;
        set
        {
            _effect = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("EFFECT_VOLUME", _effect);
        }
    }
    private void Awake()
    {
        base.Initialize();
        myBGM = gameObject.AddComponent<AudioSource>();
        myBGM.loop = true;
        if (!PlayerPrefs.HasKey("BGM_VOLUME")) bgmVolume = 0.5f;
        else bgmVolume = PlayerPrefs.GetFloat("BGM_VOLUME");
        // 설정한 값이 없으면 배경음 설정을 0.5로, 설정한 값이 있다면 설정한 값으로

        if (!PlayerPrefs.HasKey("EFFECT_VOLUME")) _effect = 0.5f;
        else _effect = PlayerPrefs.GetFloat("EFFECT_VOLUME");
        // 설정한 값이 없으면 효과음 설정을 0.5로, 설정한 값이 있다면 설정한 값으로
    }

    public void PlayBGM(AudioClip clip, float relatve)
    {
        myBGM.clip = clip;
        relativeVol = relatve;
        SetRelativeBGM();
        myBGM.Play();
    }

    public void SetRelativeBGM()
    {
        myBGM.volume = absoluteBGMSound * relativeVol;
    }
}
