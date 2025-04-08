 using UnityEngine;

public class MoveFinish : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float pivotYpos;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (rb.isKinematic) return;
        
        if (!GameManager.isPuzzle && Mathf.Approximately(rb.linearVelocity.magnitude, 0f)
            && Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, pivotYpos + 0.1f))
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
                
            transform.parent = hit.transform;
            Vector3 finalPos = transform.position;
            finalPos.y = hit.transform.position.y + 0.5f + pivotYpos;
            transform.position = finalPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().isTrigger = false;
    }
}
