 using UnityEngine;

public class MoveFinish : MonoBehaviour
{
    Rigidbody rb;
    Collider myCol;
    [SerializeField] float pivotYpos;
    [SerializeField] LayerMask dropableMask;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
        this.enabled = false;
    }
    // Update is called once per frame

    private void OnCollisionStay(Collision collision)
    {
        
        if (Mathf.Approximately(rb.linearVelocity.magnitude, 0f))
        {
            Collider[] overlap = Physics.OverlapSphere(transform.position - new Vector3(0, pivotYpos, 0), 0.1f, dropableMask); 
            if(overlap.Length > 1)
            {
                    Rigidbody[] rbArray = GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody rbEle in rbArray)
                {
                    FixedJoint joint = rbEle.GetComponent<FixedJoint>();
                    if (joint != null) joint.connectedBody = null;
                    Destroy(joint);

                    rbEle.useGravity = false;
                    rbEle.isKinematic = true;

                    Collider rbCol = rbEle.GetComponent<Collider>();
                    if (rbCol.isTrigger) rbCol.isTrigger = false;

                    BoxCollider boxCol = GetComponent<BoxCollider>();
                    if (boxCol != null) boxCol.size = new Vector3(1.0f, 1.0f, 1.0f);
                }
                for(int i = 0; i < overlap.Length; i++)
                {
                    if (overlap[i] != myCol)
                    {
                        transform.parent = overlap[i].transform;
                        Vector3 finalPos = transform.position;
                        finalPos.y = overlap[i].transform.position.y + 0.5f + pivotYpos;
                        transform.position = finalPos;
                        this.enabled = false;
                    }
                }
            }
        }
    }
}
