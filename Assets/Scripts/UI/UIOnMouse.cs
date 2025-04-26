using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseCheck : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]Animator anim;
    [SerializeField]AudioSource DragAudio;
    float abosouluteEffVolume;
    [SerializeField, Range(0f, 1f)] float relativeEffVolume = 0.5f;
    
    private void OnEnable()
    {
        anim.SetTrigger("OpenMenu");
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("OnMouse", true);
        if (abosouluteEffVolume != SoundManager.Instance.effectVolume)
        {
            abosouluteEffVolume = SoundManager.Instance.effectVolume;
            DragAudio.volume = abosouluteEffVolume * relativeEffVolume;
            //효과음의 크기를 설정창에서 저장한 크기로 맞춤
        }
        DragAudio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("OnMouse", false);
        DragAudio.Stop();
    }
}
