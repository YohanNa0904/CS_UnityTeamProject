using UnityEngine;

// 드래그로 이동하는 스크립트의 부모 스크립트
public class StateSet : MonoBehaviour
{
    private State myState = State.Create;
    [SerializeField] protected float floatDist = 0f; // 드래그 시 떠있을 기준 높이를 정함
    protected Rigidbody rb = null;

    public enum State
    {
        Create, Stop, Drag, Drop
    }

    protected void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        switch (myState)
        {
            case State.Stop:
                StopSet();
                break;

            case State.Drag:
                OnDragSet();
                break;

            case State.Drop:
                EndDragSet();
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Stop:
                break;

            case State.Drag:
                Rotate();
                OnDragPro();
                break;

            case State.Drop:
                EndDragPro();
                break;
        }
    }

    protected virtual void Start()
    {
        ChangeState(State.Stop);
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        // 시작했을 때 useGravity를 끄고, isKinematic을 켜서 오브젝트가 충돌해도 안 움직이게 해줌
    }

    private void Update() 
    {
        StateProcess(); 
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) return;
        // 마우스 우클릭이나 휠버튼으로 꾹 눌러도 상태 변경이 안 되도록 처리
        if (GameManager.isPuzzle && myState == State.Stop) ChangeState(State.Drag);
        // static 변수 isPuzzle을 통해 퍼즐 모드인지 확인
    }

    public void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)) return;
        // 마우스 우클릭이나 휠버튼을 떼도 상태 변경이 안 되도록 처리
        if (myState == State.Drag) ChangeState(State.Drop);

    }
    protected virtual void StopSet() { } //StopState 스크립트에서 override할 가상 함수
    protected virtual void OnDragSet() { } //DragState 스크립트에서 override할 가상 함수
    protected virtual void EndDragSet() { }//DropStateGravity 스크립트에서 override할 가상 함수
    
    void Rotate()
    {
        EnterRotate();
        RotateMove();
    }

    protected virtual void EnterRotate() { } //DragRotate 스크팁트에서 override할 가상 함수
    protected virtual void RotateMove() { }//DragRotate 스크팁트에서 override할 가상 함수
    protected virtual void OnDragPro() { }//DragState 스크립트에서 override할 가상 함수
    protected virtual void EndDragPro() { }//DropStateGravity 스크립트에서 override할 가상 함수 
}