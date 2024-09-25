using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float smoothSpeed = 0.125f;

    private Vector3 offset; // to maintain an offset relative to the player

    private void Start()
    {
        // Store the initial offset
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    private void LateUpdate() 
    {
        // Desired position with offset
        Vector3 desiredPosition = player.position + offset;

        // Smooth transition between current position and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
