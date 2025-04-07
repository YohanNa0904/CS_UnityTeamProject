using UnityEngine;

public class FallCheck : MonoBehaviour
{
    Rigidbody rb;
    MoveFinish moFi;
    Collider col;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moFi = GetComponent<MoveFinish>();
        col = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if(rb.linearVelocity.y < 0 && Physics.Raycast(transform.position, Vector3.down, 1.0f))
        {
            if (!moFi.enabled) moFi.enabled = true;
            if (col.isTrigger) col.isTrigger = false;
        }
    }
}
