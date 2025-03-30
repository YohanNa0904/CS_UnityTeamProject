using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float _bgm = 0f;
    public float bgmVolume
    {
        get => _bgm;
        set 
        {
            _bgm = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("BGM_VOLUME", _bgm);
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
        _bgm = PlayerPrefs.GetFloat("BGM_VOLUME");
        _effect = PlayerPrefs.GetFloat("EFFECT_VOLUME");
    }
}
