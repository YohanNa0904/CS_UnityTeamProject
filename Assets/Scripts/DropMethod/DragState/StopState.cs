using UnityEngine;

public class StopState : StateSet
{
    protected Collider[] myCols = null;
    protected override void StopSet()
    {
        if (myCols == null) myCols = GetComponentsInChildren<Collider>();
        if (!myCols[0].enabled)
        {
            for(int i = 0;i < myCols.Length; i++)
            {
                myCols[i].enabled = true;
            }
        }
        if (GameManager.isDrag) GameManager.isDrag = false;
        // static 변수 GameManager.isDrag를 변경, drag 상태가 아닌 것으로 설정.
        DollStandby();
    }

    protected virtual void DollStandby()
    {

    }
}
