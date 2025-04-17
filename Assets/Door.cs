using System.Collections;
using UnityEngine;
using static Interaction;

public class Door : MonoBehaviour, IPlayerInteraction
{
    public GameObject Key;
    public GameObject DoorKeySlot;

    public GameObject Player;
    [SerializeField]
    AudioSource DoorOpenAudio;
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
        Key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // ���� ��ȣ�ۿ� �� ���踦 �ٽ� ������ �� �ְ� ��


        Player.GetComponentInParent<PlayerMove2>().enabled = false; // ��� �߿� �÷��̾ �������� ���ϰ�
        Player.GetComponent<Animator>()?.SetFloat("Speed", 0.0f);
        Player.GetComponent<Animator>()?.SetTrigger("UseKey"); // ���踦 ���� �ִϸ��̼� ����    

        yield return new WaitForSeconds(1.5f);

        Key.transform.SetParent(null); // ���谡 ���̻� �÷��̾ �� �������
        Vector3 dir = (DoorKeySlot.transform.position - Key.transform.position).normalized;
        float dist = Vector3.Distance(DoorKeySlot.transform.position, Key.transform.position);
        while (dist > 0.01f) // 0�� �ƴ϶� �ִ��� ������ �� ���� �̵�
        {
            float targetDist = Time.deltaTime * 1.4f;
            Key.transform.position = Vector3.Lerp(Key.transform.position, DoorKeySlot.transform.position, targetDist);
            Key.transform.rotation = Quaternion.Lerp(Key.transform.rotation, DoorKeySlot.transform.rotation, targetDist);
            dist = Vector3.Distance(DoorKeySlot.transform.position, Key.transform.position);
            yield return null;
        }
        Key.transform.position = DoorKeySlot.transform.position; // �뷫���� �̵��� ���� �� ��Ȯ�� ��ġ�� ����
        Key.transform.rotation = DoorKeySlot.transform.rotation;
        Player.GetComponentInParent<PlayerMove2>().enabled = true; // ����� ������ ������ �� �ְ�

        //----------------------------------------------
        // ���踦 �� ä�� ���� ��ȣ�ۿ� �� �ؾ� �� ����
        // ���� �����ٴ��� �ϴ� �׷��͵� ����
        yield return GameTime.GetWait(2.0f);
        gameObject.GetComponent<Animator>().SetTrigger("Open");
        //DoorOpenAudio.Play();
        Key.transform.SetParent(DoorKeySlot.transform);
        Key = null;
        DoorKeySlot = null;
        //----------------------------------------------
    }
}
