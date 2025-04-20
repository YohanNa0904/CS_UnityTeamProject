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
        RelativeVolume(audio, ObjectDragCilp, relativeDragVolume);
    }

    public void RotateSound(AudioSource audio)
    {
        RelativeVolume(audio, ObjectRotateCilp, relativeRotateVolume);
    }

    void RelativeVolume(AudioSource audio, AudioClip audioClip, float relativeVol)
    {
        bool volChange = false;
        if (audio.clip != audioClip)
        { 
            audio.clip = audioClip;
            volChange = true;
        }
        if (absoulteEffVoi != SoundManager.Instance.effectVolume)
        {
            absoulteEffVoi = SoundManager.Instance.effectVolume;
            volChange = true;
        }
        
        if(volChange) audio.volume = absoulteEffVoi * relativeVol;
        
        audio.Play();
    }
    public void StopClip(AudioSource audio)
    {
        if (audio.isPlaying) audio.Stop();
    }

}
