using UnityEngine;

public class DragRotate : DragState
{
    protected bool IsRotation = false; // 현재 회전 상태 여부 확인
    Transform puzzleCameraArm = null;
    protected PuzzleCamMove camMove = null; // PuzzleCamMove 활성화 여부 결정
    
    protected override void EnterRotate()
    {
        //드래그 상태일 때 호출하는 함수
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (puzzleCameraArm == null) puzzleCameraArm = Camera.allCameras[0].transform.parent;
            IsRotation = !IsRotation;
            if (camMove == null) camMove = puzzleCameraArm.GetComponent<PuzzleCamMove>();
            if (camMove.enabled) camMove.enabled = false;
            else camMove.enabled = true;
            //회전 상태와 캠이동 컴포넌트를 상태를 변경
        }
    }
    protected override void RotateMove()
    {
        if (IsRotation)
        {
            if (Input.GetKeyDown(KeyCode.W)) RotateDir(puzzleCameraArm.right);
            else if (Input.GetKeyDown(KeyCode.S)) RotateDir(-puzzleCameraArm.right);
            else if (Input.GetKeyDown(KeyCode.A)) RotateDir(puzzleCameraArm.up);            
            else if (Input.GetKeyDown(KeyCode.D)) RotateDir(-puzzleCameraArm.up);            
            else if (Input.GetKeyDown(KeyCode.Q)) RotateDir(puzzleCameraArm.forward);            
            else if (Input.GetKeyDown(KeyCode.E)) RotateDir(-puzzleCameraArm.forward);
        }
        // 회전상태일 때 wasd로 회전
    }

    void RotateDir(Vector3 dir)
    {
        Vector3 rotKeyDown = dir;
        RotateAngleSet(ref rotKeyDown.x, ref rotKeyDown.y, ref rotKeyDown.z);
        //카메라가 바라보는 방향을 각 축에서 90도씩 회전할 수 있도록 보정해줌
        transform.Rotate(rotKeyDown * 90.0f, Space.World);
        //보정해준 좌표를 90도씩 회전함.
        DragAudioManager.Instance.RotateSound(audioSo);
        // DragObjcetManger 오브젝트에 있는 DragAudioManager 인스펙터에서 정한 Rotate clip으로 소리를 재생
    }

    void RotateAngleSet(ref float x, ref float y, ref float z)
    {
        // x,y,z 좌표 중 절대값이 가장 큰 값을 찾음
        if (Mathf.Abs(x) >= Mathf.Abs(y) && Mathf.Abs(x) >= Mathf.Abs(z))
        {
            CompareAxis(ref x, ref y, ref z);
        }

        else if (Mathf.Abs(y) > Mathf.Abs(x) && Mathf.Abs(y) >= Mathf.Abs(z))
        {
            CompareAxis(ref y, ref x, ref z);
        }

        else if (Mathf.Abs(z) > Mathf.Abs(x) && Mathf.Abs(z) > Mathf.Abs(y))
        {
            CompareAxis(ref z, ref x, ref y);
        }

    }

    void CompareAxis(ref float a, ref float b, ref float c)
    {
        //절대값이 제일 큰 좌표 값을 부호에 따라 -1,1로 바꾸고, 나머지 좌표는 0으로 함
        //값형 변수를 함수 내에서 바꾼 값을 함수 외부에서도 사용해야 함으로 ref를 붙여줌.
        if (a < 0) a = -1;
        else a = 1;
        b = 0f;
        c = 0f;
    }

}
