using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapPlayerMove : MonoBehaviour
{
    
   
    public bool onStage;
    public int connectSceanNum;
    [SerializeField] Animator myAnim;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform myModel;
    [SerializeField] Transform[] stageList;
    [SerializeField] TextMeshPro[] stageTextList;
    [SerializeField] GameObject[] stageLock;
    Vector3 inputDir;

    int temp;
    void Start()
    {

        onStage = false;
        for (int i = 0; i < stageList.Length; i++)
        {
            stageList[i].name = $"{i + 3}";
            stageTextList[i].enabled = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //키보드 입력
        Move();


        temp = DataManager.instance.nowMap.clear;
        StartCoroutine(UnLockStage());

        if(DataManager.instance.nowMap.clear >= connectSceanNum && onStage)
        {
            if(Input.GetKey(KeyCode.Space))
            LoadSystem.LoadScene(connectSceanNum);
            temp++;
            StopCoroutine(UnLockStage());
        }
        
    }
    private void Move()
    {
        bool isMove =  inputDir.magnitude !=0; //이동중인지 확인
        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized; //카메라의 전방 벡터
            Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;   //카메라의 오른쪽 벡터
            Vector3 moveDir = lookForward * inputDir.y + lookRight * inputDir.x; //이동 방향 설정
        
            Quaternion viewRot = Quaternion.LookRotation(moveDir.normalized); //이동 방향으로 회전

            myModel.transform.rotation = Quaternion.Lerp(myModel.transform.rotation, viewRot, Time.deltaTime * 20.0f); //모델 회전

            myAnim.SetFloat("Speed", moveDir.magnitude); //애니메이션 속도 설정
        }
    }
    void OnTriggerEnter(Collider other)
    {
        connectSceanNum = Convert.ToInt32(other.transform.name);
        onStage = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        onStage = false;     
    }
    IEnumerator UnLockStage()
{
    for(int i = 3; i <= DataManager.instance.nowMap.clear; i++)
    {
        stageTextList[i - 3].enabled = true;
        stageLock[i - 3].SetActive(false);
        
        yield return GameTime.GetWait(0.1f);
    }
}
}
