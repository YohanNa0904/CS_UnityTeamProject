using UnityEngine;

public class SpringGravity : DragAlpha
{
    LayerMask dollLayer = default;
    DollStandbyAnim frontDoll = null;
    protected override void OnDragSet()
    {
        pivotDist = 0.5f;
        base.OnDragSet();
    }

    protected override void DollStandby()
    {
        if (dollLayer == default) dollLayer = LayerMask.GetMask("Doll");
        if (frontDoll != null && frontDoll.IsButton) frontDoll.StandbyCheck();

        Collider[] list = Physics.OverlapBox(transform.position - transform.right,
              new Vector3(0.45f, 0.45f, 45f), transform.rotation, dollLayer);
        foreach(Collider col in list)
        {
            frontDoll = col.GetComponent<DollStandbyAnim>();
            if(!frontDoll.IsButton) frontDoll.StandbyCheck();
        }
    }
}
