using UnityEngine;
using static DollInteraction;

public class IronSpring : MonoBehaviour, IDollInteraction
{
    public LayerMask pushLayer;
    [SerializeField] float pushPowar = 900.0f;
    Rigidbody rb;
    LayerMask playerMask;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponentInParent<Rigidbody>();
        playerMask = LayerMask.GetMask("Player");
    }
    public void Interact()
    {
        if (!rb.isKinematic && rb.transform.parent != null) return;
        GetComponent<Animator>()?.SetTrigger("Using");
        audioSource.Play();
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

            if ((1 << col.gameObject.layer & playerMask) == 0)
            {
                rbArray[0].transform.parent = null;

                if (rbArray.Length > 0)
                {
                    foreach (Rigidbody rb in rbArray)
                    {
                        MeshCollider meshCol = rb.GetComponent<MeshCollider>();
                        if (meshCol != null) meshCol.convex = true;

                        rb.isKinematic = false;
                        rb.useGravity = true;

                        BoxCollider boxCol = rb.GetComponent<BoxCollider>();
                        if (boxCol != null && boxCol.size == new Vector3(1f, 1f, 1f))
                        {
                            boxCol.size *= 0.95f;
                        }
                    }
                }

                if (rbArray.Length > 1)
                {
                    FixedJoint[] jointArray = new FixedJoint[rbArray.Length];
                    for (int i = 0; i < rbArray.Length; i++)
                    {
                        if (rbArray[i].GetComponent<FixedJoint>() == null)
                            jointArray[i] = rbArray[i].gameObject.AddComponent<FixedJoint>();
                        else jointArray[i] = rbArray[i].GetComponent<FixedJoint>();

                        if (i != 0) jointArray[i].connectedBody = rbArray[0];
                    }
                    jointArray[0].connectedBody = rbArray[1];
                }
            }
            
            rbArray[0].AddForce(transform.up * pushPowar,ForceMode.Impulse);  // 찾아진 오브젝트에 릿지드 바디가 있으면 해당 오브젝트를 밈
            if(rbArray[0].GetComponent<MoveFinish>() != null) rbArray[0].GetComponent<MoveFinish>().enabled = true;
        }
    }
}