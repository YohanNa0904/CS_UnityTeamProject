using UnityEngine;

public class BgmCilp : MonoBehaviour
{
    public AudioClip bgmClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.PlayBGM(bgmClip);
    }

}
