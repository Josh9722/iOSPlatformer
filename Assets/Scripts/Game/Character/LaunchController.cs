using System.Collections;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    // +++++++++++++++++++++++++ ATTRIBUTES +++++++++++++++++++++++++
    [SerializeField] private float launchPower = 3f;     // Power of the launch
    
    private float maxDragDistance = 20f;  // Maximum drag distance in world units
    private float dragThreshold = 5f;   // Minimum drag distance in pixels to start dragging

    private Rigidbody2D rb;
    private Vector2 launchDirection;
    private Vector3 dragStartPosition;  // World position where drag starts
    private bool isDragging = false;
    private bool isThresholdMet = false;  // Check if threshold distance is met
    private Camera mainCamera;  // Cache the camera reference
    private Vector3 currentDragPosition;  // Current position during dragging for lerping

    // +++++++++++++++++++++++++ UNITY METHODS +++++++++++++++++++++++++
    private void Awake()
    {
        InitializeComponents();
    }

    private void Update()
    {
        HandleInput();
    }

    // +++++++++++++++++++++++++ INITIALIZATION +++++++++++++++++++++++++
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the GameObject.");
            enabled = false;  // Disable the script to avoid errors
            return;
        }

        // Ensure the Rigidbody2D is set to dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not found in the scene.");
            enabled = false;  // Disable the script to avoid errors
            return;
        }
    }

    // +++++++++++++++++++++++++ INPUT HANDLING +++++++++++++++++++++++++
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
        else if (isDragging)
        {
            ProcessDrag();
        }
    }

    // +++++++++++++++++++++++++ DRAG METHODS +++++++++++++++++++++++++
    private void StartDrag()
    {
        dragStartPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);  // Store the initial world position of the mouse
        isDragging = true;
        isThresholdMet = false;
        currentDragPosition = transform.position;  // Initialize current drag position for lerping
    }

    private void ProcessDrag()
    {
        if (!isThresholdMet)
        {
            CheckDragThreshold();
        }
    }

    private void CheckDragThreshold()
    {
        Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dragVector = currentMousePosition - dragStartPosition;

        if (dragVector.magnitude > dragThreshold)
        {
            isThresholdMet = true;
        }
    }

    private void UpdateLaunchDirection(Vector3 endMousePosition)
    {
        Vector3 dragVector = endMousePosition - dragStartPosition;

        // Clamp the drag distance to the maximum drag distance in world units
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        launchDirection = dragVector;
    }

    private void EndDrag()
    {
        if (!isDragging)
        {
            return;
        }
        isDragging = false;

        if (isThresholdMet)
        {
            Vector3 endMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);  // World position where drag ends
            UpdateLaunchDirection(endMousePosition);

            // Debug magnitude of vector 
            Debug.Log("Magnitude of movement swipe: " + launchDirection.magnitude);
            ApplyLaunchForce();
        }

        ResetDrag();
    }

    private void ApplyLaunchForce()
    {
        rb.velocity = Vector2.zero;  // Reset any existing velocity
        rb.AddForce(-launchDirection.normalized * launchDirection.magnitude * launchPower, ForceMode2D.Impulse);
    }

    private void ResetDrag()
    {
        launchDirection = Vector2.zero;
        isThresholdMet = false;
    }
}
