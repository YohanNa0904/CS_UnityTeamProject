using UnityEngine;
using static DollInteraction;

public class DollCase : AnimProperty, IDollInteraction
{
    public GameObject door;

    public void Interact()
    {
        if (Physics.Raycast(door.transform.position - new Vector3(0, 1, 0), door.transform.forward, 1.0f)) myAnim.SetTrigger("DollCaseOpen");
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
