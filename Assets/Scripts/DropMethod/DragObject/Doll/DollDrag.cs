using UnityEngine;

public class DollDrag : DragAlpha
{
    DollStandbyAnim dollStan;
    
    protected override void RotateMove()
    {
        if (IsRotation)
        {
            if (Input.GetKeyDown(KeyCode.A)) transform.Rotate(0, 90.0f, 0, Space.World);
            else if (Input.GetKeyDown(KeyCode.D)) transform.Rotate(0, -90.0f, 0, Space.World);
            
        }
    }
    protected override void DollStandby()
    {
        if (dollStan == null) dollStan = GetComponent<DollStandbyAnim>();
        dollStan.StandbyCheck();
    }
}
