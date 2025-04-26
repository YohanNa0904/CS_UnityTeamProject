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
        // 이미 인형이 준비자세를 취했는데 스프링을 다른 곳으로 옮겼을 때, 기본 자세로 되돌림
        Collider[] list = Physics.OverlapBox(transform.position - transform.right,
              new Vector3(0.45f, 0.45f, 45f), transform.rotation, dollLayer);
        // 버튼 앞에 인형이 있는 지 확인함
        foreach(Collider col in list)
        {
            frontDollAnim = col.GetComponent<DollStandbyAnim>();
            if(!frontDollAnim.IsButton) frontDollAnim.StandbyCheck();
            //인형이 준비자세를 취하지 않았다면, 준비자세를 취하게 함.
        }
    }
}
