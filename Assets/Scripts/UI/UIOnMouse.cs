using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseCheck : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]Animator anim;
    [SerializeField]AudioSource DragAudio;
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
        DragAudio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("OnMouse", false);
        DragAudio.Stop();
    }
}
