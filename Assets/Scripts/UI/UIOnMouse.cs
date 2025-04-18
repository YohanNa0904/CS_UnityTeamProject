using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseCheck : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]Animator anim;
    [SerializeField]AudioSource DragAudio;
    float abosouluteEffVolume;
    [SerializeField, Range(0f, 1f)] float relativeEffVolume = 1f;
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
        
        abosouluteEffVolume = SoundManager.Instance.effectVolume;
        DragAudio.volume = abosouluteEffVolume * relativeEffVolume;
        
        DragAudio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("OnMouse", false);
        DragAudio.Stop();
    }
}
