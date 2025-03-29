using System.Collections;
using UnityEngine;
using static Interaction;

public class Key : MonoBehaviour, IPlayerInteraction
{
    public GameObject Player;
    public GameObject KeySlot;

    public void Interact()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // ��ȣ�ۿ� ������ ������ ��ȣ�ۿ��� ������Ʈ�� �������� ����
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;

        Player.GetComponent<Animator>().SetTrigger("Catch"); //��� �ִϸ��̼�
        Player.gameObject.GetComponentInParent<PlayerMove2>().enabled = false; // ��� ���� �߿� �� �����̰�
        Player.GetComponent<Animator>().SetFloat("Speed", 0.0f);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // �ִϸ��̼��� ��� ����� ���� �� ���谡 �ٽ� �����̰� ��

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

        float dist = Vector3.Distance(transform.position, KeySlot.transform.position); // �Ÿ� ���

        while (dist > 0.035f) // ��Ȯ�� 0�� �ƴ϶� �ִ��� ������� �� ���� �ݺ�
        {
            float targetDist = Time.deltaTime * 3.0f;

            transform.position = Vector3.Lerp(transform.position, KeySlot.transform.position, targetDist);
            transform.rotation = Quaternion.Lerp(transform.rotation, KeySlot.transform.rotation, targetDist);

            dist = Vector3.Distance(transform.position, KeySlot.transform.position); // ���� �Ÿ� ����
            yield return null;
        }

        transform.position = KeySlot.transform.position; // �̵��� ���� �ڿ� ��Ȯ�� ��ġ�� ����
        transform.rotation = KeySlot.transform.rotation; // �̵��� ���� �ڿ� ȸ���� ����
        transform.SetParent(KeySlot.transform); // �̵��� ���� �ڿ� ��� �÷��̾ ����ٴϰ�
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // �÷��̾��� �տ� ���� �ڷδ� �������̰�(�տ��� �� ��������)

        GetComponentInParent<PlayerMove2>().enabled = true; // ���踦 ��� ������ ������ �÷��̾ �ٽ� ������ �� �ְ�
    }
}
