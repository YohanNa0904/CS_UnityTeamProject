using UnityEngine;
using static Interaction;

public class Key : MonoBehaviour, IPlayerInteraction
{
    public GameObject Player;
    public void Interact()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // 상호작용 성공한 시점에 상호작용한 오브젝트의 움직임을 멈춤
        Player.GetComponent<Animator>().SetTrigger("Catch"); //잡는 애니메이션
        Player.gameObject.GetComponentInParent<PlayerMove2>().enabled = false; // 잡는 동작 중엔 못 움직이게
        Player.GetComponent<Animator>().SetFloat("Speed", 0.0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
