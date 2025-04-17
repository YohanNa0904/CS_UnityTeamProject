using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
   AudioSource myBGM;
    float absoluteBGMSound; 
    [SerializeField] float relativeSoundNum = 1.0f;
    public float bgmVolume
    {
        get => absoluteBGMSound;
        set 
        {
            absoluteBGMSound = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("BGM_VOLUME", absoluteBGMSound);
            myBGM.volume = absoluteBGMSound * relativeSoundNum;
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
             myBGM.volume = PlayerPrefs.GetFloat("BGM_VOLUME");
        }
        if (!PlayerPrefs.HasKey("EFFECT_VOLUME")) _effect = 0.3f;
        else _effect = PlayerPrefs.GetFloat("EFFECT_VOLUME");
    }

    public void PlayBGM(AudioClip clip)
    {
        myBGM.clip = clip;
        myBGM.Play();
    }
}
