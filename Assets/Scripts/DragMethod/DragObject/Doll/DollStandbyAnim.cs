using UnityEngine;

public class DollStandbyAnim : MonoBehaviour
{
    Animator myAnim;
    [SerializeField] LayerMask button;
    public bool IsButton { get; private set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void StandbyCheck()
    {
        if (IsButton) IsButton = false;
        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 0.5f + transform.forward * 0.5f
            , new Vector3(0.6f, 0.9f, 1.0f) * 0.5f, transform.rotation);
        // 인형 앞에 버튼이 있는 지 확인
        foreach(Collider col in list)
        {
            if ((1 << col.gameObject.layer & button) != 0)
            {
                IsButton = true;
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) myAnim.SetTrigger("Use");
                // 인형이 기본자세라면 준비자세로 설정함
            }
        }

        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Use") && !IsButton) myAnim.SetTrigger("StandUp");
        //버튼이 앞에 없고, 인형이 준비자세라면 인형을 기본 자세로 바꿈
    }
}
