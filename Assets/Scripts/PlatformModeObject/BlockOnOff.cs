using UnityEngine;

public class BlockOnOff : MonoBehaviour
{
    [field: SerializeField] public GameObject onOffBlock { get; private set; }
    [SerializeField] LayerMask dropAble;
    [SerializeField] LayerMask doll;
    int myLayer;

    void Start()
    {
        myLayer = (int)Mathf.Pow(2, gameObject.layer);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & dropAble) != 0 && other.transform.position.y > transform.position.y)
        {
            onOffBlock.SetActive(true);
            Collider col = GetComponent<Collider>();
            col.isTrigger = false;

            SpringGravity spring = other.GetComponent<SpringGravity>();
            spring.dropAble += myLayer;

            DollDrag doll = other.GetComponentInChildren<DollDrag>();
            if (doll != null) doll.dropAble += myLayer;
        }
    }
}
