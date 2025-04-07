using UnityEngine;

public class BlockOnOff : MonoBehaviour
{
    [field: SerializeField] public GameObject onOffBlock { get; private set; }
    [SerializeField] LayerMask dropAble;
    [SerializeField] LayerMask doll;
    private void OnTriggerEnter(Collider other)
    {
        int myLayer = (int)Mathf.Pow(2, gameObject.layer);
        if ((1 << other.gameObject.layer & dropAble) != 0)
        {
            SpringGravity spring = other.GetComponent<SpringGravity>();
            spring.dropAble += myLayer;
        }

        else if((1 << other.gameObject.layer & doll) != 0)
        {
            DollDrag doll = other.GetComponent<DollDrag>();
            doll.dropAble += myLayer;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & dropAble) != 0)
        {
            onOffBlock.SetActive(true);
            Collider col = GetComponent<Collider>();
            col.isTrigger = false;
        }
    }
}
