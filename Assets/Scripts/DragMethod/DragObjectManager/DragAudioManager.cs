using UnityEngine;

public class DragAudioManager : MonoBehaviour
{
    public static DragAudioManager Instance = null;
    [SerializeField] AudioClip ObjectDragCilp;
    [SerializeField] AudioClip ObjectRotateCilp;
    float absoulteEffVoi;
    [SerializeField, Range(0f, 1f)] float relativeDragVolume = 1f;
    [SerializeField, Range(0f, 1f)] float relativeRotateVolume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void DragSound(AudioSource audio)
    {
        if (audio.clip != ObjectDragCilp) audio.clip = ObjectDragCilp;
        if (absoulteEffVoi != SoundManager.Instance.effectVolume) 
            absoulteEffVoi = SoundManager.Instance.effectVolume;
            audio.volume = absoulteEffVoi * relativeDragVolume;
        audio.Play();
    }

    public void RotateSound(AudioSource audio)
    {
        if (audio.clip != ObjectRotateCilp) audio.clip = ObjectRotateCilp;
        if (absoulteEffVoi != SoundManager.Instance.effectVolume)
            absoulteEffVoi = SoundManager.Instance.effectVolume;
        audio.volume = absoulteEffVoi * relativeRotateVolume;
        audio.Play();
    }
    public void StopClip(AudioSource audio)
    {
        if (audio.isPlaying) audio.Stop();
    }

}
