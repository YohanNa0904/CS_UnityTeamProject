
using UnityEngine;


public class PlayerMove2 : AnimProperty
{
    //------������ �� ���� ����---------
    public bool onGround { get; set; } = true; // 
    bool jumpForce = false; //�����ϴ� ���� ������ �����ϴ� ����

    //-------------------------------
    public Transform myModel;
    public float moveSpeed = 3.0f;
    Vector3 jumpDir = Vector3.zero;
    public Transform cameraTransform;
    public Vector3 inputDir { get; private set; } = Vector3.zero;
    int jumpCount = 2;
    Rigidbody rb = null;
    float maxSpeed = 1.0f;
    [SerializeField] float jumpPower = 6.0f;
    [SerializeField] AudioSource JumpAudio;
    [SerializeField] AudioSource StepAudio;
    bool isMove = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //Ű���� �Է�
        Move();
        if(inputDir.magnitude != 0)
        {
            if (!isMove)
            {
                StepAudio.Play();
                isMove = true;
            }
        }
        else
        {
            StepAudio.Stop();
            isMove = false;
        }
        if (jumpCount != 0 && Input.GetKeyDown(KeyCode.Space))
        {
            JumpAudio.Play();
            jumpForce = true;
            jumpCount--;
        }

        if (!onGround) // ���߿� ���� ���� ���ݾ� �̵��� �� �ְ�
        {
            if (Input.GetKey(KeyCode.W))
            {
                jumpDir += myModel.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                jumpDir += myModel.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                jumpDir += myModel.forward;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                jumpDir += myModel.forward;
            }
            jumpDir.Normalize();
        }
    }
    private void Move()
    {
        bool isMove = inputDir.magnitude != 0; //�̵������� Ȯ��
        Vector3 moveDir = Vector3.zero;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized; //ī�޶��� ���� ����
            Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;   //ī�޶��� ������ ����
            moveDir = lookForward * inputDir.y + lookRight * inputDir.x; //�̵� ���� ����

            Quaternion viewRot = Quaternion.LookRotation(moveDir.normalized); //�̵� �������� ȸ��

            myModel.rotation = Quaternion.Lerp(myModel.rotation, viewRot, Time.deltaTime * 20.0f); //�� ȸ�� 

        }
        float rootMotionSpeed = moveDir.magnitude;
        rootMotionSpeed = Mathf.Clamp(rootMotionSpeed, 0, maxSpeed);
        myAnim.SetFloat("Speed", rootMotionSpeed); //�ִϸ��̼� �ӵ� ����
    }
    public void SetMaxSpeed(float speed)
    {
        if(maxSpeed != speed) maxSpeed = speed;   
    }
    private void FixedUpdate()
    {
        if (jumpForce)
        {
            myAnim.SetTrigger("OnJump");
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpForce = false;
            onGround = false;
        }
        if (!onGround)
        {
            float Speed = moveSpeed * Time.fixedDeltaTime;
            transform.Translate(jumpDir * Speed, Space.Self);
            jumpDir = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (onGround) return;
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            onGround = true; //���� ���·� ����            
            myAnim.SetTrigger("OnLanding"); // jump3 �ִϸ��̼� ����
            jumpCount = 2;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!onGround) return;

        float veloY = rb.linearVelocity.y;
        if (Mathf.Abs(veloY) > 0.1f && jumpCount == 2)
        {
            // ���� ������ �� y������ �������ٸ�
            onGround = false; // ü�� ���·� ����
            myAnim.SetTrigger("OnAir");
            // ���� ����(����Ű�� ���� ���)�� �ƴ϶�� jump2 �ִϸ��̼� ����
            jumpCount--;
        }
    }
    
}