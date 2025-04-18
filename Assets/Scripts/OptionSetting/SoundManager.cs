using Unity.Profiling;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource myBGM;
    float absoluteBGMSound; 
    float relativeBGMVolume { get; set; }
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
        else
        {
             bgmVolume = PlayerPrefs.GetFloat("BGM_VOLUME");
        }
        if (!PlayerPrefs.HasKey("EFFECT_VOLUME")) _effect = 0.3f;
        else _effect = PlayerPrefs.GetFloat("EFFECT_VOLUME");
    }

    public void PlayBGM(AudioClip clip, float relatve)
    {
        myBGM.clip = clip;
        relativeBGMVolume = relatve;
        SetRelativeBGM();
        myBGM.Play();
    }

    public void SetRelativeBGM()
    {
        myBGM.volume = absoluteBGMSound * relativeBGMVolume;
    }
}
