using UnityEngine;

public class SpringGravity : DragAlpha
{
    [SerializeField]LayerMask dollLayer;
    DollStandbyAnim frontDollAnim = null;
    protected override void OnDragSet()
    {
        pivotDist = 0.5f;
        base.OnDragSet();
    }

    protected override void DollStandby()
    {
        if (frontDollAnim != null && frontDollAnim.IsButton) frontDollAnim.StandbyCheck();

        Collider[] list = Physics.OverlapBox(transform.position - transform.right,
              new Vector3(0.45f, 0.45f, 45f), transform.rotation, dollLayer);

        foreach(Collider col in list)
        {
            frontDollAnim = col.GetComponent<DollStandbyAnim>();
            if(!frontDollAnim.IsButton) frontDollAnim.StandbyCheck();
        }
    }
}
