using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotationController : MonoBehaviour
{
    [SerializeField] private float correctionSpeed = -1f; // The speed at which the rotation is corrected towards 0

    private Rigidbody2D rb;

    private void Awake()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CorrectRotation();
    }

    private void CorrectRotation()
    {
        // Calculate the shortest difference between the current rotation and the target rotation (0 degrees on the z-axis)
        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.z, 0f);

        // Calculate the corrective torque based on the angle difference
        float correctiveTorque = -angleDifference * correctionSpeed;

        // Apply the corrective torque to the Rigidbody2D
        rb.AddTorque(correctiveTorque);
    }
}
