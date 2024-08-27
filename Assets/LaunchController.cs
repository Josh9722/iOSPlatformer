using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
        // +++++++++++++++++++++++++ ATTRIBUTES +++++++++++++++++++++++++
    [SerializeField] private float maxDragDistance = 3f; // Maximum drag distance
    [SerializeField] private float launchPower = 10f;    // Power of the launch
    private Rigidbody2D rb;
    private Vector2 launchDirection;
    private Vector3 startPosition;
    private bool isDragging = false;

    // +++++++++++++++++++++++++ METHODS +++++++++++++++++++++++++
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;  // Store the initial position
    }

    private void Update()
    {
        // Check if the left mouse button is being held down
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseDrag();
        }

        if (isDragging)
        {
            DragCharacter();
        }
    }

    private void StartDrag()
    {
        isDragging = true;
        rb.isKinematic = true;  // Disable physics while dragging
    }

    private void DragCharacter()
    {
        // Convert mouse position to world position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragVector = mousePosition - (Vector2)startPosition;

        // Clamp the drag distance to the maximum drag distance
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        // Update character position
        transform.position = startPosition + (Vector3)dragVector;
        launchDirection = dragVector;
    }

    private void ReleaseDrag()
    {
        isDragging = false;
        rb.isKinematic = false;  // Re-enable physics

        // Apply force in the opposite direction of the drag
        rb.AddForce(-launchDirection * launchPower, ForceMode2D.Impulse);

        // Reset the launch direction
        launchDirection = Vector2.zero;
    }

    private void OnMouseDown()
    {
        // Only start dragging if the character is clicked on
        StartDrag();
    }
}
