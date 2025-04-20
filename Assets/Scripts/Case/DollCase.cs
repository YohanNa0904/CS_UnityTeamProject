using UnityEngine;
using static DollInteraction;

public class DollCase : AnimProperty, IDollInteraction
{
    public GameObject Doll;

    public void Interact()
    {
        if (!Physics.BoxCast(transform.position + new Vector3(0, -0.5f, 0.5f),new Vector3(0.4f,0.9f,0.4f),Vector3.forward, Quaternion.identity,1f))
        {
            Doll.transform.SetParent(null);
            myAnim.SetTrigger("DollCaseOpen");

            GetComponent<Collider>().enabled = false;
        }

    }
}
