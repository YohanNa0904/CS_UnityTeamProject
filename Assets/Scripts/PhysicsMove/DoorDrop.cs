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
            myAnim.SetTrigger("Drop");
            isDrop = true;
            rb.isKinematic = true;
            col[1].enabled = true;
        }
    }
}
