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

    private void OnCollisionStay(Collision collision)
    {
        
        if (Mathf.Approximately(rb.linearVelocity.magnitude, 0f))
        {
            Collider[] overlap = Physics.OverlapSphere(transform.position - new Vector3(0, pivotYpos, 0), 0.1f, dropableMask); 
            //오브젝트가 지면에 닿았다면
            if(overlap.Length > 1)
            {
                Rigidbody[] rbArray = GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody rbEle in rbArray)
                {
                    FixedJoint joint = rbEle.GetComponent<FixedJoint>();
                    if (joint != null) joint.connectedBody = null;
                    Destroy(joint);
                    // 스프링으로 밀면서 생성한 조인트 제거

                    rbEle.useGravity = false;
                    rbEle.isKinematic = true;

                    BoxCollider boxCol = GetComponent<BoxCollider>();
                    if (boxCol != null) boxCol.size = new Vector3(1.0f, 1.0f, 1.0f);
                    //스프링으로 밀면서 줄인 콜라이더 크기를 다시 되돌림
                }
                for(int i = 0; i < overlap.Length; i++)
                {
                    if (overlap[i] != myCol)
                    {
                        transform.parent = overlap[i].transform;
                        //착지한 물체를 오브젝트의 부모로 설정
                        Vector3 finalPos = transform.position;
                        finalPos.y = overlap[i].transform.position.y + 0.5f + pivotYpos;
                        //오브젝트의 y좌표를 조정
                        transform.position = finalPos;
                        this.enabled = false;
                    }
                }
            }
        }
    }
}
