using System.Collections;
using UnityEngine;
using static Interaction;

public class Key : MonoBehaviour, IPlayerInteraction
{
    public GameObject Player;
    public GameObject KeySlot;

    public void Interact()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // 상호작용 성공한 시점에 상호작용한 오브젝트의 움직임을 멈춤
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;

        Player.GetComponent<Animator>().SetTrigger("Catch"); //잡는 애니메이션
        Player.gameObject.GetComponentInParent<PlayerMove2>().enabled = false; // 잡는 동작 중엔 못 움직이게
        Player.GetComponent<Animator>().SetFloat("Speed", 0.0f);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // 애니메이션의 잡는 모션이 나올 때 열쇠가 다시 움직이게 함

        StartCoroutine(KeyCatch());


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator KeyCatch()
    {
        yield return new WaitForSeconds(1.0f);

        float dist = Vector3.Distance(transform.position, KeySlot.transform.position); // 거리 계산

        while (dist > 0.035f) // 정확히 0이 아니라 최대한 가까워질 때 까지 반복
        {
            float targetDist = Time.deltaTime * 3.0f;

            transform.position = Vector3.Lerp(transform.position, KeySlot.transform.position, targetDist);
            transform.rotation = Quaternion.Lerp(transform.rotation, KeySlot.transform.rotation, targetDist);

            dist = Vector3.Distance(transform.position, KeySlot.transform.position); // 남은 거리 갱신
            yield return null;
        }

        transform.position = KeySlot.transform.position; // 이동이 끝난 뒤에 정확한 위치로 스냅
        transform.rotation = KeySlot.transform.rotation; // 이동이 끝난 뒤에 회전도 맞춤
        transform.SetParent(KeySlot.transform); // 이동이 끝난 뒤엔 계속 플레이어를 따라다니게
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // 플레이어의 손에 붙은 뒤로는 못움직이게(손에서 안 떨어지게)

        GetComponentInParent<PlayerMove2>().enabled = true; // 열쇠를 잡는 동작이 끝나면 플레이어가 다시 움직일 수 있게
    }
}
