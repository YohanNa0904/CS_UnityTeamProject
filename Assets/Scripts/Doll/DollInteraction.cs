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
        Collider[] list = Physics.OverlapBox(transform.position + transform.up * 0.7f + transform.forward * 0.5f, new Vector3(0.4f, 1.4f, 1.0f) * 0.5f, transform.rotation);
        foreach (Collider col in list)
        {
            //----------------------------------------------------------------------------------------------------------------------------------------------------------

            if ((1 << col.gameObject.layer & Spring1_Button) != 0)
            // 상호작용 대상이 'Button1' 일 때
            {
                IDollInteraction spring = col.gameObject.GetComponentInParent<IDollInteraction>();
                if (spring != null)
                {
                    spring.Interact();
                }
            }
            if ((1 << col.gameObject.layer & Spring2_Button) != 0)
            // 상호작용 대상이 'Button2' 일 때
            {
                IDollInteraction spring2 = col.gameObject.GetComponentInParent<IDollInteraction>();
                if (spring2 != null)
                {
                    spring2.Interact();
                }
            }

            if ((1 << col.gameObject.layer & DollCaseMask) != 0)
            // 상호작용 대상이 'dollcase' 일 때
            {
                IDollInteraction dollCase = col.GetComponent<IDollInteraction>();
                if (dollCase != null)
                {
                    dollCase.Interact();
                }
            }
        }
    }
    
}
