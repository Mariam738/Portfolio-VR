using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
public class PianoKeyController : MonoBehaviour
{
    private Rigidbody rb;
    private HingeJoint hinge;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();

        // Freeze unwanted rotations and positions
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;

        // Configure hinge joint
        hinge.useLimits = true;
        JointLimits limits = hinge.limits;
        limits.min = -5f;   // how far the key can go down
        limits.max = 0f;    // resting position
        hinge.limits = limits;

        // Make sure the hinge axis is correct (local X, Y, or Z depending on your model)
        hinge.axis = Vector3.right; // adjust to match your piano key’s pivot axis
    }

    void Update()
    {
        // Optional: reset if physics glitches
        if (transform.localRotation.eulerAngles.y > 0.01f ||
            transform.localRotation.eulerAngles.z > 0.01f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0f, 0f);
        }
    }
}
