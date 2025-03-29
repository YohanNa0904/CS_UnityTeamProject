using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Interaction : AnimProperty
{

    //-------------------------
    // 없애면 안 되는 거
    public LayerMask Key;
    public LayerMask Door;
    public LayerMask Grow;
    public LayerMask PickAxe;
    public LayerMask Wall;
    public LayerMask dollCase;

    GameObject InteractTarget;
    //-------------------------

    GameObject GrowSlot;
    GameObject WallSlot;
    [SerializeField]
    GameObject wallObj;


    public interface IPlayerInteraction
    {
        void Interact();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider[] list = Physics.OverlapBox(transform.position + transform.up * 0.7f + transform.forward * 0.5f, new Vector3(0.4f ,1.4f ,1.0f) * 0.5f, transform.rotation);
            foreach (Collider col in list)
            {
            //----------------------------------------------------------------------------------------------------------------------------------------------------------
                if ((1 << col.gameObject.layer & Key ) != 0)
                    // 상호작용 대상이 'key' 일 때
                {
                    IPlayerInteraction Key = col.GetComponent<IPlayerInteraction>();
                    if ( Key != null )
                    {
                        InteractTarget = col.gameObject;
                        Key.Interact();
                    }
                }
                if ((1 << col.gameObject.layer & PickAxe) != 0)
                {
                    InteractTarget = col.gameObject;

                    col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    myAnim.SetTrigger("Catch");
                    GetComponentInParent<PlayerMove2>().enabled = false;
                    myAnim.SetFloat("Speed", 0.0f);

                }
            //----------------------------------------------------------------------------------------------------------------------------------------------------------


            //----------------------------------------------------------------------------------------------------------------------------------------------------------

                if (InteractTarget != null && (1 << InteractTarget.gameObject.layer & Key) != 0) // 열쇠를 가지고 있다면
                {
                    if ((1 << col.gameObject.layer & Door) != 0)
                    // 상호작용 대상이 'door' 일 때
                    {
                        IPlayerInteraction Door = col.GetComponent<IPlayerInteraction>();
                        //if (Door != null)
                        {
                            Door.Interact();
                        }
                    }

                    else if ((1 << col.gameObject.layer & Grow) != 0)

                    {
                        GrowSlot = col.gameObject;
                        myAnim.SetTrigger("UseKey"); // 열쇠를 쓰는 애니메이션 실행
                    }
                }
                if(InteractTarget != null && (1 << InteractTarget.gameObject.layer & PickAxe) != 0)
                {
                    if ((1 << col.gameObject.layer & Wall) != 0)
                    {
                        WallSlot = col.gameObject;
                        myAnim.SetTrigger("UseKey");
                    }
                }
            //----------------------------------------------------------------------------------------------------------------------------------------------------------


            }
        }
    }

    void PickAxeInteract()
    {
        //StartCoroutine(KeyCatch(PickAxe));
    }

    void UseKey() // 애니메이션 이벤트로 호출하는 함수
    {
        if (GrowSlot != null) StartCoroutine(UsingGrowingPosion());
        if (WallSlot != null) StartCoroutine(UsingPickAxe());
    }

    IEnumerator UsingGrowingPosion()
    {
        InteractTarget.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // 문과 상호작용 시 열쇠를 다시 움직일 수 있게 함
        InteractTarget.transform.SetParent(null); // 열쇠가 더이상 플레이어를 안 따라오게


        gameObject.GetComponentInParent<PlayerMove2>().enabled = false; // 모션 중엔 플레이어가 움직이지 못하게
        myAnim.SetFloat("Speed", 0.0f);

        Vector3 dir = (GrowSlot.transform.position - InteractTarget.transform.position).normalized;
        float dist = Vector3.Distance(GrowSlot.transform.position, InteractTarget.transform.position);
        while (dist > 0.01f) // 0이 아니라 최대한 근접할 때 까지 이동
        {
            float targetDist = Time.deltaTime * 1.4f;
            InteractTarget.transform.position = Vector3.Lerp(InteractTarget.transform.position, GrowSlot.transform.position, targetDist);
            InteractTarget.transform.rotation = Quaternion.Lerp(InteractTarget.transform.rotation, GrowSlot.transform.rotation, targetDist);
            dist = Vector3.Distance(GrowSlot.transform.position, InteractTarget.transform.position);
            yield return null;
        }
        
        gameObject.GetComponentInParent<PlayerMove2>().enabled = true; // 모션이 끝나면 움직일 수 있게

        //----------------------------------------------
        yield return GameTime.GetWait(2.0f);
        GrowSlot.GetComponentInParent<Animator>().SetTrigger("OnGrow");
        InteractTarget.transform.SetParent(GrowSlot.transform);
        InteractTarget = null;
        GrowSlot = null;
        //----------------------------------------------
    }
    IEnumerator UsingPickAxe()
    {
        InteractTarget.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        InteractTarget.transform.SetParent(null); // 열쇠가 더이상 플레이어를 안 따라오게
        
        gameObject.GetComponentInParent<PlayerMove2>().enabled = false; // 모션 중엔 플레이어가 움직이지 못하게
        myAnim.SetFloat("Speed", 0.0f);

        Vector3 dir = (WallSlot.transform.position - InteractTarget.transform.position).normalized;
        float dist = Vector3.Distance(WallSlot.transform.position, InteractTarget.transform.position);
        while (dist > 0.01f) // 0이 아니라 최대한 근접할 때 까지 이동
        {
            float targetDist = Time.deltaTime * 1.4f;
            InteractTarget.transform.position = Vector3.Lerp(InteractTarget.transform.position, WallSlot.transform.position, targetDist);
            InteractTarget.transform.rotation = Quaternion.Lerp(InteractTarget.transform.rotation, WallSlot.transform.rotation, targetDist);
            dist = Vector3.Distance(WallSlot.transform.position, InteractTarget.transform.position);
            yield return null;
        }
        
        gameObject.GetComponentInParent<PlayerMove2>().enabled = true; // 모션이 끝나면 움직일 수 있게

        yield return GameTime.GetWait(1.0f);
        Destroy(wallObj);
        InteractTarget = null;
        WallSlot = null;

    }

    public void SpeedReset()
        // 애니메이션 이벤트로 호출되는 함수
    {
        myAnim.SetFloat("Speed", 0.0f);
        // 애니메이션이 끝날 때 호출
    }
}


