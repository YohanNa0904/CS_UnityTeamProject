using UnityEngine;

public class DollStandbyAnim : MonoBehaviour
{
    Animator myAnim;
    [SerializeField] LayerMask spring1_Button;
    [SerializeField] LayerMask spring2_Button;
    bool isButton = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void StandbyCheck()
    {
        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 0.5f + transform.forward * 0.5f
            , new Vector3(0.6f, 0.9f, 1.0f) * 0.5f, transform.rotation);

        foreach(Collider col in list)
        {
            if ((1 << col.gameObject.layer & spring1_Button) != 0
                || (1 << col.gameObject.layer & spring2_Button) != 0)
            {
                isButton = true;
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) myAnim.SetTrigger("Use");
            }
        }
        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Use") && !isButton) myAnim.SetTrigger("StandUp");
        isButton = false;
    }
}
