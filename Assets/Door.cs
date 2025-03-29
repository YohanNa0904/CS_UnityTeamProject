using System.Collections;
using UnityEngine;
using static Interaction;

public class Door : MonoBehaviour, IPlayerInteraction
{
    public GameObject Key;
    public GameObject DoorKeySlot;

    public GameObject Player;

    public void Interact()
    {
        StartCoroutine(UsingKey());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    IEnumerator UsingKey()
    {
        Key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // 문과 상호작용 시 열쇠를 다시 움직일 수 있게 함


        Player.GetComponentInParent<PlayerMove2>().enabled = false; // 모션 중엔 플레이어가 움직이지 못하게
        Player.GetComponent<Animator>()?.SetFloat("Speed", 0.0f);
        Player.GetComponent<Animator>()?.SetTrigger("UseKey"); // 열쇠를 쓰는 애니메이션 실행    

        yield return new WaitForSeconds(1.5f);

        Key.transform.SetParent(null); // 열쇠가 더이상 플레이어를 안 따라오게
        Vector3 dir = (DoorKeySlot.transform.position - Key.transform.position).normalized;
        float dist = Vector3.Distance(DoorKeySlot.transform.position, Key.transform.position);
        while (dist > 0.01f) // 0이 아니라 최대한 근접할 때 까지 이동
        {
            float targetDist = Time.deltaTime * 1.4f;
            Key.transform.position = Vector3.Lerp(Key.transform.position, DoorKeySlot.transform.position, targetDist);
            Key.transform.rotation = Quaternion.Lerp(Key.transform.rotation, DoorKeySlot.transform.rotation, targetDist);
            dist = Vector3.Distance(DoorKeySlot.transform.position, Key.transform.position);
            yield return null;
        }
        Key.transform.position = DoorKeySlot.transform.position; // 대략적인 이동이 끝난 후 정확한 위치에 스냅
        Key.transform.rotation = DoorKeySlot.transform.rotation;
        Player.GetComponentInParent<PlayerMove2>().enabled = true; // 모션이 끝나면 움직일 수 있게

        //----------------------------------------------
        // 열쇠를 든 채로 문과 상호작용 시 해야 할 동작
        // 문이 열린다던가 하는 그런것들 실행
        yield return GameTime.GetWait(2.0f);
        gameObject.GetComponent<Animator>().SetTrigger("Open");
        Key.transform.SetParent(DoorKeySlot.transform);
        Key = null;
        DoorKeySlot = null;
        //----------------------------------------------
    }
}
