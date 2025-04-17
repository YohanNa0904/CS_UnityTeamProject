using UnityEngine;

public class DragAudioManager : MonoBehaviour
{
    public static DragAudioManager Instance = null;
    [SerializeField] AudioClip ObjectDragCilp;
    [SerializeField] AudioClip ObjectRotateCilp;
    private void Awake()
    {
        Instance = this;
    }

    public void DragSound(AudioSource audio)
    {
        if (audio.clip != ObjectDragCilp) audio.clip = ObjectDragCilp;
        if (audio.volume != SoundManager.Instance.effectVolume) audio.volume = SoundManager.Instance.effectVolume;
        if (!audio.isPlaying) audio.Play();
    }

    public void StopClip(AudioSource audio)
    {
        if (audio.isPlaying) audio.Stop();
    }

}
