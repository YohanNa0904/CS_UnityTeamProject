using UnityEngine;

public class UIAudio : MonoBehaviour
{
    AudioSource audioSource;
    void Awake()
    {
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }
}
