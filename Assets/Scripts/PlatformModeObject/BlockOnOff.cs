using UnityEngine;

public class BlockOnOff : MonoBehaviour
{
    [field: SerializeField] public GameObject onOffBlock { get; private set; }
    //[field : SerializeField] public LayerMask triggerMask { get; private set; }
    private void OnTriggerExit(Collider other)
    {
        /*
        if ((1 << other.gameObject.layer & triggerMask) != 0)
        {
            transform.gameObject.SetActive(true);
        }
        */
        onOffBlock.SetActive(true);
    }
}
