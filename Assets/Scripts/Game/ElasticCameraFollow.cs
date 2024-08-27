using UnityEngine;

public class ElasticCameraFollow : MonoBehaviour
{
    // +++++++++++++++++++++++++ ATTRIBUTES +++++++++++++++++++++++++
    [SerializeField] private Transform target; // The character to follow

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void FixedUpdate ()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

	}

}
