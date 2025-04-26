using UnityEngine;

public class DollDrag : DragAlpha
{
    DollStandbyAnim dollStan;
    
    protected override void RotateMove()
    {
        if (IsRotation)
        {
            if (Input.GetKeyDown(KeyCode.A)) DollRotateOper(90f);
            else if (Input.GetKeyDown(KeyCode.D)) DollRotateOper(-90f);
        }
    }

    void DollRotateOper(float Yangle)
    {
        transform.Rotate(0, Yangle, 0, Space.World);
        DragAudioManager.Instance.RotateSound(audioSo);
        // DragObjcetManger 오브젝트에 있는 DragAudioManager 인스펙터에서 정한 Rotate clip으로 소리를 재생
    }
    protected override void DollStandby()
    {
        //드래그가 끝났을 때 호출하는 함수
        if (dollStan == null) dollStan = GetComponent<DollStandbyAnim>();
        dollStan.StandbyCheck();
        //인형이 준비자세를 취할 지 판별하는 함수
    }
}
