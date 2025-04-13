using UnityEngine;

public class StopState : StateSet
{
    protected Collider myCol = null;
    protected override void StopSet()
    {
        if (myCol == null) myCol = GetComponent<Collider>();
        if (!myCol.enabled) myCol.enabled = true;
        if (GameManager.isDrag) GameManager.isDrag = false;
        // static 변수 GameManager.isDrag를 변경, drag 상태가 아닌 것으로 설정.
        DollStandby();
    }

    protected virtual void DollStandby()
    {

    }
}
