using System.Collections;
using UnityEngine;

public class DollInteraction : AnimProperty
{
    public LayerMask Spring1_Button;
    public LayerMask Spring2_Button;
    public LayerMask DollCaseMask;
    protected GameObject InteractTarget;


    public interface IDollInteraction
    {
        void Interact();
    }
        
    protected void DollInteract()
    {
        if(GameManager.isPuzzle) return;
        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 0.7f + transform.forward * 0.5f, new Vector3(0.4f, 1.4f, 1.0f) * 0.5f, transform.rotation);
        foreach (Collider col in list)
        {
            //----------------------------------------------------------------------------------------------------------------------------------------------------------

            if ((1 << col.gameObject.layer & Spring1_Button) != 0)
            // ��ȣ�ۿ� ����� 'Button1' �� ��
            {
                IDollInteraction spring = col.gameObject.GetComponentInParent<IDollInteraction>();
                if (spring != null)
                {
                    spring.Interact();
                }
            }
            if ((1 << col.gameObject.layer & Spring2_Button) != 0)
            // ��ȣ�ۿ� ����� 'Button2' �� ��
            {
                IDollInteraction spring2 = col.gameObject.GetComponentInParent<IDollInteraction>();
                if (spring2 != null)
                {
                    spring2.Interact();
                }
            }

            if ((1 << col.gameObject.layer & DollCaseMask) != 0)
            // ��ȣ�ۿ� ����� 'dollcase' �� ��
            {
                IDollInteraction dollCase = col.GetComponent<IDollInteraction>();
                if (dollCase != null)
                {
                    dollCase.Interact();
                }
            }
        }
}
