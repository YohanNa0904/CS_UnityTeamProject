using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseCheck : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]Animator anim;
    [SerializeField]AudioSource DragAudio;
    float abosouluteEffVolume;
    [SerializeField, Range(0f, 1f)] float relativeEffVolume = 0.5f;
    void Awake()
    {
        anim.SetBool("OnMouse", false);
    }
    
    private void OnEnable()
    {
        anim.SetBool("OnMouse", false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("OnMouse", true);
        if (abosouluteEffVolume != SoundManager.Instance.effectVolume)
        {
            abosouluteEffVolume = SoundManager.Instance.effectVolume;
            DragAudio.volume = abosouluteEffVolume * relativeEffVolume;
        }
        DragAudio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("OnMouse", false);
        DragAudio.Stop();
    }
}
