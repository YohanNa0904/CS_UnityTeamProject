using UnityEngine;

public class DragState : StopState
{
    protected Vector3 dragStartPos = Vector3.zero; //드래그 시작할 때 위치 정보
    protected Vector3 dragStartRot = Vector3.zero; //드래그 시작할 때 회전 정보
    public LayerMask dropAble; // 드랍할 수 있는 바닥 레이어
    //protected float floatYpos = 0.0f; //드래그 시 띄울 y 좌표
    protected bool canDrop = false;
    protected float pivotDist { get; set; } = 0.0f;
    protected Transform canDropTrans = null;

    protected override void OnDragSet()
    {
        if (transform.parent != null) transform.parent = null;
        dragStartPos = transform.position;
        dragStartRot = transform.eulerAngles; // 드래그 시작할 때의 위치와 회전 정보를 저장 
        transform.position += Vector3.up * floatDist; // 드래그한 오브젝트를 띄움
        //floatYpos = transform.position.y; // 띄운 y좌표를 저장함
        for(int i = 0; i < myCols.Length; i++)
        {
            myCols[i].enabled = false;
        }
        if (!GameManager.isDrag) GameManager.isDrag = true;
        // static 변수 GameManager.isDrag를 변경, drag 상태로 설정.
        //if (floatDist != standardFloatDist) floatDist = standardFloatDist;
        // 띄울 높이가 기준 높이와 다르다면 띄울 높이를 기준 높이로 변경
        Debug.Log("Drag");
    }

    protected override void OnDragPro()
    {
        Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, dropAble))
        {
            canDropTrans = rayHit.transform;
            Vector3 terminalPos = rayHit.transform.position;
            terminalPos.y = canDropTrans.position.y + 0.5f + floatDist + pivotDist;
            transform.position = terminalPos;

             if (Physics.BoxCast(terminalPos + Vector3.up * 2.0f, new Vector3(0.4f, 0.4f, 0.4f), Vector3.down,
                    out RaycastHit boxHit, Quaternion.identity, Mathf.Infinity)) 
                        
             {
                if ((1 << boxHit.transform.gameObject.layer & dropAble) != 0 ) DropJudge(true);

                else DropJudge(false);
                    
             }
        }
        else DropJudge(false);
    }
    private void DropJudge(bool tf)
    {
        canDrop = tf;
        DragAlphaTF(tf);
    }
    protected virtual void DragAlphaTF(bool tf)
    {
        
    }

}
