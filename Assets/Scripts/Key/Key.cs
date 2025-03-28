using UnityEngine;
using static Interaction;

public class Key : MonoBehaviour, IPlayerInteraction
{
    public GameObject Player;
    public void Interact()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // ��ȣ�ۿ� ������ ������ ��ȣ�ۿ��� ������Ʈ�� �������� ����
        Player.GetComponent<Animator>().SetTrigger("Catch"); //��� �ִϸ��̼�
        Player.gameObject.GetComponentInParent<PlayerMove2>().enabled = false; // ��� ���� �߿� �� �����̰�
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
