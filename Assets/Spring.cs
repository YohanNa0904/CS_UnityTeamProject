using System.Collections;
using UnityEngine;
using static DollInteraction;

public class Spring : AnimProperty, IDollInteraction

// ���� ���� ������Ʈ�� ���̴� ��ũ��Ʈ
{
    public LayerMask pushAble;
    public GameObject Panel;

    bool On = false;


    public void Interact()
    {
        GetComponentInParent<Animator>()?.SetBool("Spring1On", !GetComponentInParent<Animator>().GetBool("Spring1On"));
    }

    public void OnPush()
    {
        StartCoroutine(OnButton());
    }
    public IEnumerator OnButton() // �ִϸ��̼� �̺�Ʈ�� ȣ���ϴ� �Լ�
    {
        On = true;
        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 1.5f, new Vector3(0.45f, 0.45f, 0.45f), transform.rotation, pushAble); // �Լ��� ����� �� ���� ���� �͵��� ã��
        // �ǳ� �������� ��ĭ �̵��� ������ü ����� ���� ����� ����
        foreach (Collider col in list)
        {
            Transform orgTf = col.GetComponentInParent<Transform>().parent;

            while (On)
            {
                col.GetComponent<Transform>()?.SetParent(Panel.transform);
                yield return null;
            }

            col.GetComponent<Transform>()?.SetParent(orgTf);
        }
    }


    public void OutButton() // �ִϸ��̼� �̺�Ʈ�� ȣ���ϴ� �Լ�
    {
        On = false;
    }


}
