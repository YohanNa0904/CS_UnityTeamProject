using UnityEngine;

public class SpringGravity : DragAlpha
{
    DollStandbyAnim frontDoll = null;
    LayerMask dollLayer = default;
    //BoxCollider boxCol = null;
    //����� �� ���� collider�� �ִ� ������Ʈ���� ������ ���߱� ������
    //isTriger�� Ȱ��ȭ�ϱ� ���� BoxCollider ������Ʈ ������ ����

    protected override void OnDragSet()
    {
        pivotDist = 0.5f;
        base.OnDragSet();
    }

    protected override void DollStandby()
    {
        if (dollLayer == default) dollLayer = LayerMask.GetMask("Doll"); 
        if (frontDoll != null) frontDoll.StandbyCheck();
        Collider[] list = Physics.OverlapBox(transform.position - transform.right
            , new Vector3(0.45f, 0.45f, 45f), transform.rotation);
        foreach(Collider col in list)
        {
            if ((1 << col.gameObject.layer & dollLayer) != 0)
            {
                frontDoll = col.GetComponent<DollStandbyAnim>();
                frontDoll.StandbyCheck();
            }
        }
    }
}
