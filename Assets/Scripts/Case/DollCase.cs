using UnityEngine;
using static DollInteraction;

public class DollCase : AnimProperty, IDollInteraction
{
    public GameObject Doll;
    public Transform dollCase;

    public void Interact()
    {
        if (!Physics.BoxCast(dollCase.position + new Vector3(0, 1, 0.5f),new Vector3(0.4f,0.9f,0.4f),Vector3.forward, Quaternion.identity,1f))
        {
            Doll.transform.SetParent(null);
            myAnim.SetTrigger("DollCaseOpen");

            GetComponent<Collider>().enabled = false;
        }

    }
}
