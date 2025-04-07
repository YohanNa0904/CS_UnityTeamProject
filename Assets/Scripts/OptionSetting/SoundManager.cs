using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource myBGM;
    
    public float bgmVolume
    {
        get => myBGM.volume;
        set 
        {
            myBGM.volume = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("BGM_VOLUME", myBGM.volume);
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
        else myBGM.volume = PlayerPrefs.GetFloat("BGM_VOLUME");

        if (!PlayerPrefs.HasKey("EFFECT_VOLUME")) _effect = 0.3f;
        else _effect = PlayerPrefs.GetFloat("EFFECT_VOLUME");
    }

    public void PlayBGM(AudioClip clip)
    {
        myBGM.clip = clip;
        myBGM.Play();
    }
}
