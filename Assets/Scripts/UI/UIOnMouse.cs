using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseCheck : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("onMouse");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("out");
    }
}
