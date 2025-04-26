using UnityEngine;

public class DragState : StopState
{
    protected Vector3 dragStartPos = Vector3.zero; //드래그 시작할 때 위치 정보
    protected Vector3 dragStartRot = Vector3.zero; //드래그 시작할 때 회전 정보
    public LayerMask dropAble; // 드랍할 수 있는 바닥 레이어
    //protected float floatYpos = 0.0f; //드래그 시 띄울 y 좌표
    protected bool canDrop = false; // 드래그한 물체를 놓을 수 있는 지 판별하는 변수
    protected float pivotDist { get; set; } = 0.0f; // 드래그하는 오브젝트의 중심점의 y좌표(스프링은 0.5f, 인형은 0)
    protected Transform canDropTrans = null; // 드래그해서 놓을 수 있는 오브젝트의 트랜스폼을 저장
    protected AudioSource audioSo;

    protected override void Start()
    {
        base.Start();
        audioSo = GetComponent<AudioSource>();
    }

    protected override void OnDragSet()
    {
        // 드래그 시작할 때 호출하는 함수
        if (transform.parent != null) transform.parent = null; // 드래그하는 오브젝트의 부모가 있다면 부모를 null로 설정함 
        dragStartPos = transform.position;
        dragStartRot = transform.eulerAngles; // 드래그 시작할 때의 위치와 회전 정보를 저장 
        transform.position += Vector3.up * floatDist; // 드래그한 오브젝트를 띄움
        for(int i = 0; i < myCols.Length; i++)
        {
            myCols[i].enabled = false;
        }
        //드래그하는 오브젝트 및 자식들의 콜라이더를 비활성화함

        if (!GameManager.isDrag) GameManager.isDrag = true;
        // static 변수 GameManager.isDrag를 변경, drag 상태로 설정.
        //if (floatDist != standardFloatDist) floatDist = standardFloatDist;
        // 띄울 높이가 기준 높이와 다르다면 띄울 높이를 기준 높이로 변경
        
    }

    protected override void OnDragPro()
    {
        Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
        // 카메라에서 마우스 커서 위치로 레이져를 쏨
        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, dropAble))
        {
            canDropTrans = rayHit.transform;
            //레이져를 맞은 오브젝트를 트랜스폼을 변수에 저장
            Vector3 terminalPos = rayHit.transform.position;
            // 드래그 중인 오브젝트가 이동할 위치를 저장
            terminalPos.y = canDropTrans.position.y + 0.5f + floatDist + pivotDist;
            // 이동할 위치의 y 좌표를 조정해줌
            if (transform.position != terminalPos)
            {
                // 현재 오브젝트의 위치와 이동할 위치가 다르다면
                transform.position = terminalPos;
                // 오브젝트를 이동할 위치로 옮김
                DragAudioManager.Instance.DragSound(audioSo);
                // DragObjcetManger 오브젝트에 있는 DragAudioManager 인스펙터에서 정한 드래그 clip으로 소리를 재생

                if (Physics.BoxCast(terminalPos + Vector3.up * 1.5f, new Vector3(0.4f, 0.4f, 0.4f), Vector3.down,
                       out RaycastHit boxHit, Quaternion.identity, Mathf.Infinity))
                       //움직일 위치에서 박스를 y 아래 방향으로 쏴서 드래그한 위치에 다른 오브젝트가 있는 지 확인
                {
                    if ((1 << boxHit.transform.gameObject.layer & dropAble) != 0) DropJudge(true);
                    // 다른 오브젝트 없이, 드래그 가능한 레이어를 가진 오브젝트만 있으면 드랍 가능한 것으로 판별
                    else DropJudge(false);

                }
            }
            else DropJudge(true); // 현재 위치와 이동할 위치가 같으면 드랍 가능한 것으로 판별
        }
        else DropJudge(false); // 마우스 커서가 드랍할 수 있는 오브젝트에 위치하지 않으면 드랍 불가능한 것으로 판별
        //preMousePos = Input.mousePosition;
    }
    private void DropJudge(bool tf)
    {
        canDrop = tf;
        DragAlphaTF(tf);
    }
    protected virtual void DragAlphaTF(bool tf) { }
    //DragAlpha 스크립트에서 override할 가상 함수
}