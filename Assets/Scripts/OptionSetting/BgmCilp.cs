using UnityEngine;

public class BgmCilp : MonoBehaviour
{
    [SerializeField] AudioClip bgmClip;
    [SerializeField,Range(0f,1f)] float relativeVolume = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.PlayBGM(bgmClip,relativeVolume);
    }

}
