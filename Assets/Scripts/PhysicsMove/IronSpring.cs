using UnityEngine;
using static DollInteraction;

public class IronSpring : MonoBehaviour, IDollInteraction
{
    public LayerMask pushLayer;
    [SerializeField] float pushPowar = 900.0f;

    public void Interact()
    {
        GetComponent<Animator>()?.SetTrigger("Using");
    }

    public void OnPush() // 애니메이션 이벤트로 호출하는 함수
    {
        FixedJoint joint = GetComponentInParent<FixedJoint>();
        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);
        } 
        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 1.5f, new Vector3(0.45f, 0.45f, 0.45f), transform.rotation, pushLayer); // 함수가 실행될 때 위에 놓인 것들을 찾음
        // 판넬 방향으로 한칸 이동한 정육면체 모양의 감지 모양을 만듦
        foreach (Collider col in list)
        {
            Rigidbody[] rbArray = col.GetComponentsInChildren<Rigidbody>();
            
            if (rbArray.Length > 0)
            {
                foreach(Rigidbody rb in rbArray)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                }

                if(rbArray.Length > 1)
                {
                    FixedJoint[] joints = new FixedJoint[rbArray.Length];
                    for(int i = 0; i < rbArray.Length; i++)
                    {
                        if (rbArray[i].GetComponent<FixedJoint>() == null)
                            joints[i] = rbArray[i].gameObject.AddComponent<FixedJoint>();
                        else joints[i] = rbArray[i].GetComponent<FixedJoint>();
                        
                        if (i != 0) joints[i].connectedBody = rbArray[0]; 
                    }
                    joints[0].connectedBody = rbArray[1];
                }

                rbArray[0].AddForce(transform.up * pushPowar,ForceMode.VelocityChange);  // 찾아진 오브젝트에 릿지드 바디가 있으면 해당 오브젝트를 밈
            } 
        }
    }
}