using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource stepAudio;
    public AudioSource getJumpAudio { get => jumpAudio; }
    public AudioSource getStepAudio { get => stepAudio; }

    float abosouluteEffVolume;
    [SerializeField, Range(0f, 1f)] float relativeJumpVolume = 1f;
    [SerializeField, Range(0f, 1f)] float relativeStepVolume = 1f;
    // 상대적 음량 조절할 수치 설정
    public void JumpRelativeVol()
    {
        RelativeVolume(jumpAudio, relativeJumpVolume);
    }

    public void StepRelativeVol()
    {
        RelativeVolume(stepAudio, relativeStepVolume);
    }
    void RelativeVolume(AudioSource audio, float relativeVol)
    {
        if (abosouluteEffVolume != SoundManager.Instance.effectVolume)
        {
            abosouluteEffVolume = SoundManager.Instance.effectVolume;
            audio.volume = abosouluteEffVolume * relativeVol;
        }
        audio.Play();
    }


}
