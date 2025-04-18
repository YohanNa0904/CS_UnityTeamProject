using UnityEngine;
using static DollInteraction;

public class DollCase : AnimProperty, IDollInteraction
{
    public GameObject door;
    public GameObject Doll;

    public void Interact()
    {
        if (!Physics.Raycast(door.transform.position - new Vector3(0, 1, 0), -door.transform.up, 1.0f))
        {
            Debug.Log("DollCase");
            Doll.transform.SetParent(null);
            myAnim.SetTrigger("DollCaseOpen");

            GetComponent<Collider>().enabled = false;
            transform.parent.GetComponent<Collider>().enabled = false;
        }

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
