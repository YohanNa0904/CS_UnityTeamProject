using UnityEngine;
using static DollInteraction;

public class Spring2 : MonoBehaviour, IDollInteraction
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
        //움직이면서 부모가 있으면 리턴
        //(2층을 쌓아서 같이 움직인다면 2층에 있는 오브젝트는 부모가 있으니 작동을 안하고 리턴)
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
        // 이미 조인트가 설정된 경우 조인트를 삭제함

        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 1.5f, new Vector3(0.45f, 0.45f, 0.45f), transform.rotation, pushLayer); // 함수가 실행될 때 위에 놓인 것들을 찾음
        // 판넬 방향으로 한칸 이동한 정육면체 모양의 감지 모양을 만듦
        foreach (Collider col in list)
        {
            Rigidbody[] rbArray = col.GetComponentsInChildren<Rigidbody>();

            if ((1 << col.gameObject.layer & playerMask) == 0)
            { 
                // 미는 오브젝트가 플레이어가 아니라면
                rbArray[0].transform.parent = null;
                // 미는 오브젝트 중 가장 아래쪽에 있는 오브젝트의 부모를 null로 설정함
                if (rbArray.Length > 0)
                {
                    foreach (Rigidbody rb in rbArray)
                    {
                        MeshCollider meshCol = rb.GetComponent<MeshCollider>();
                        if (meshCol != null) meshCol.convex = true;
                        // 메쉬콜라이더의 convex를 true로 설정
                        // true가 아니면 물리적으로 힘을 안 받아서 우물이 안 치워짐

                        rb.isKinematic = false;
                        rb.useGravity = true;

                        BoxCollider boxCol = rb.GetComponent<BoxCollider>();
                        if (boxCol != null && boxCol.size == new Vector3(1f, 1f, 1f))
                        {
                            boxCol.size *= 0.9f;
                            // 움직이면서 모서리 부분 마찰로 멈추는 것을 막기 위해 콜라이더 크기를 줄임
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
                        //조인트가 없었다면 fixedJoint 컴포넌트를 생성함
                        //조인트가 있으면 그것을 변수로 저장

                        if (i != 0) jointArray[i].connectedBody = rbArray[0];
                        // 가장 아래 오브젝트가 아니라면, 가장 아래 오브젝트로 연결해줌
                    }
                    jointArray[0].connectedBody = rbArray[1];
                    //미는 오브젝트가 2개 이상이라면 서로를 조인트로 연결함
                }
            }
            
            rbArray[0].AddForce(transform.up * pushPowar,ForceMode.Impulse);  // 찾아진 오브젝트에 릿지드 바디가 있으면 해당 오브젝트를 밈
            if(rbArray[0].GetComponent<MoveFinish>() != null) rbArray[0].GetComponent<MoveFinish>().enabled = true; // MoveFinish 컴포넌트를 활성화 시킴
        }
    }
}