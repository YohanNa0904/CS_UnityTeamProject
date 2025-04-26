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
        //드래그하는 동안 콜라이더를 비활성화시켜서, 이를 다시 활성화하는 함수

        if (GameManager.isDrag) GameManager.isDrag = false;
        // static 변수 GameManager.isDrag를 변경, drag 상태가 아닌 것으로 설정.
        DollStandby();
    }

    protected virtual void DollStandby() { } 
    //DollDrag 스크립트에서 override할 가상 함수
    //인형이 장치 작동할 준비자세를 취하도록 하는 함수
    
    
}
