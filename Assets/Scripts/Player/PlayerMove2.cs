using UnityEngine;

public class PlayerMove2 : AnimProperty
{
    //------������ �� ���� ����---------
    public bool onGround { get; set; } = true; // 
    bool jumpForce = false; // 뛰는 힘을 가할 지 판별하는 변수

    //-------------------------------
    public Transform myModel; 
    public float moveSpeed = 3.0f;
    Vector3 jumpDir = Vector3.zero;
    public Transform cameraTransform;
    Vector3 inputDir = Vector3.zero;
    int jumpCount = 2;
    Rigidbody rb = null;
    float maxSpeed = 1.0f;
    [SerializeField] float jumpPower = 6.0f;
    PlayerSound playerSound;
    bool audioPlay = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSound = GetComponent<PlayerSound>();
    }
    void Update()
    {
        inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //Ű���� �Է�
        Move();
    
        if (jumpCount != 0 && Input.GetKeyDown(KeyCode.Space))
        {
            jumpForce = true;
            jumpCount--;
        }

        if (!onGround) // 점프 중일 때
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
        if(audioPlay && Input.GetKey(KeyCode.LeftControl)||Input.GetKey(KeyCode.Escape))
        {
            playerSound.getJumpAudio.Stop();
            playerSound.getStepAudio.Stop();
            playerSound.getJumpAudio.mute = true;
            playerSound.getStepAudio.mute = true;
            
            audioPlay = false;
        }
        if(!audioPlay && Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.Escape))
        {
            playerSound.getJumpAudio.mute = false;
            playerSound.getStepAudio.mute = false;
            
            audioPlay = true;
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

            if (!playerSound.getStepAudio.isPlaying && onGround)
                playerSound.StepRelativeVol();
            //걷는 소리 재생
            else if (!onGround) playerSound.getStepAudio.Stop();
            
        }
        else playerSound.getStepAudio.Stop();
        
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
            //균일하게 점프하기 위해서 Velocity를 초기화함
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            playerSound.JumpRelativeVol();
            //점프 소리 재생
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
            //충돌한 콜라이더의 노말벡터의 y 값이 0.5보다 크면(수직에 가깝게 충돌했다면)
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
            // 점프를 안 했는데 벨로시티의 y값이 일정 값 이상으로 크면 떨어지는 것으로 판정
            onGround = false; 
            myAnim.SetTrigger("OnAir");
            //떨어지는 애니메이션 재생
            jumpCount--;
        }
    }
    
}