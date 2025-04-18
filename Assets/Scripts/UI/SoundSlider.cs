using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public enum Type { BGM, EFFECT}
    public Type type;
    public Slider mySlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (type)
        {
            case Type.BGM:
                mySlider.value = SoundManager.Instance.bgmVolume;
                mySlider.onValueChanged.AddListener(v => 
                    {
                        SoundManager.Instance.bgmVolume = v;
                        SoundManager.Instance.SetRelativeBGM();
                    }
                );
                break;

            case Type.EFFECT:
                mySlider.value = SoundManager.Instance.effectVolume;
                mySlider.onValueChanged.AddListener(v => SoundManager.Instance.effectVolume = v);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
