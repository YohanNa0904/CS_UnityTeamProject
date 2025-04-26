using UnityEngine;

public class DoorDrop : MonoBehaviour
{
    Animator myAnim;
    Rigidbody rb;
    bool isDrop = false;
    Collider[] col;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponentsInChildren<Collider>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDrop && rb.isKinematic == false)
        {
            // 스프링이 작동해서 isKisnematic이 false라면
            myAnim.SetTrigger("Drop");
            isDrop = true;
            rb.isKinematic = true;
            col[1].enabled = true;
            //DropBlock에 bone에 달린 콜라이더 활성화
        }
    }
}
