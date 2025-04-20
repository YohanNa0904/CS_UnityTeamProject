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
    }
    protected override void DollStandby()
    {
        if (dollStan == null) dollStan = GetComponent<DollStandbyAnim>();
        dollStan.StandbyCheck();
    }
}
