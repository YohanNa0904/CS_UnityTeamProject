using UnityEngine;

public class Hinge : MonoBehaviour
{
    [SerializeField] LayerMask crashMask;
    HingeJoint hingeJoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        if (hingeJoint != null)
        {
            JointSpring spring = hingeJoint.spring;
            spring.damper = 100;
            hingeJoint.spring = spring;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0, -3), transform.up * 16f, Color.yellow);

        if (Physics.Raycast(transform.position + new Vector3(0, 0, -3), transform.up, out RaycastHit hit, 16f, crashMask))
        {
            if (hingeJoint != null)
            {
                JointSpring spring = hingeJoint.spring;
                spring.damper = 100;
                hingeJoint.spring = spring;
            }
        }
        else
        {
            if (hingeJoint != null)
            {
                JointSpring spring = hingeJoint.spring;
                spring.damper = 10000;
                hingeJoint.spring = spring;
            }
        }
    }
}