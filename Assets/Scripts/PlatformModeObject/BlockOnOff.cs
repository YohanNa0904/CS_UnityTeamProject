using UnityEngine;

public class BlockOnOff : MonoBehaviour
{
    [field: SerializeField] GameObject onOffBlock;
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
            //TriggerExit를 호출한 콜라이더를 포함한 오브젝트가 지정한 레이어를 가지고 있고, 콜라이더의 y 좌표가 블럭의 y 좌표보다 위에 있다면
            onOffBlock.SetActive(true);
            Collider col = GetComponent<Collider>();
            col.isTrigger = false;

            SpringGravity spring = other.GetComponent<SpringGravity>();
            spring.dropAble += myLayer;
            //드랍할 수 있는 레이어에 블럭의 레이어를 추가함

            DollDrag doll = other.GetComponentInChildren<DollDrag>();
            if (doll != null) doll.dropAble += myLayer;
            //드랍할 수 있는 레이어에 블럭의 레이어를 추가함
        }
    }
}
