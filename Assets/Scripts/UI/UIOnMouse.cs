using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseCheck : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]Animator anim = null;
    [SerializeField]AudioSource DragAudio;
    void Awake()
    {
        anim.SetBool("OnMouse", false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("onMouse");
        anim.SetBool("OnMouse", true);
        DragAudio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("out");
        anim.SetBool("OnMouse", false);
        DragAudio.Stop();
    }
}
