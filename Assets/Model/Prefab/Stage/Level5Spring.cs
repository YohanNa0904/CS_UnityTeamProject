using UnityEngine;
using UnityEngine.Events;

public class Level5Spring : MonoBehaviour
{
    public Rigidbody rock;

    public Animator spring1;
    public Animator spring2;
    public Animator spring3;
    public Animator spring4;
    public Animator spring5;
    public Animator spring6;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rock.constraints = RigidbodyConstraints.FreezePositionX;
    }

    // Update is called once per frame
    void Update()
    {
        if (rock.gameObject.transform.position.z >= 13)
        {
            spring1.SetTrigger("Using");
            spring2.SetTrigger("Using");
            spring3.SetTrigger("Using");
            spring4.SetTrigger("Using");
            spring5.SetTrigger("Using");
            spring6.SetTrigger("Using");

            rock.isKinematic = true;
            rock.isKinematic = false;
            rock.constraints = RigidbodyConstraints.None;

            rock.gameObject.transform.position = new Vector3(2, -9.6f, 13);
            rock.AddForce(-transform.right * 5.0f, ForceMode.Impulse);
            GetComponent<Level5Spring>().enabled = false;
        }

        
    }

}
