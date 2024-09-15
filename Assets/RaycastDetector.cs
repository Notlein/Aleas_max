using UnityEngine;

public class RaycastDetector : MonoBehaviour
{
    public float raycastDistance = 10000f; // The maximum distance for the raycast
    private Vector3 initialMousePosition;
    private bool isDragging;              // Flag to indicate dragging
    private bool readyToCheckRelease;     // Flag to manage if we're ready to check for release without drag
    public float minDragDistance = 0.1f;    // Minimum distance to consider as a drag

    void Update()
    {
        // When the mouse button is pressed down, record the starting position
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
            isDragging = false;
            readyToCheckRelease = true; // Enable checking for mouse release from now
        }

        // Track if the mouse is being dragged
        if (Input.GetMouseButton(0) && readyToCheckRelease)
        {
            if (Vector3.Distance(initialMousePosition, Input.mousePosition) > minDragDistance)
            {
                isDragging = true;   // Set isDragging true if the drag distance exceeds the minimum threshold
            }
        }

        // Only consider a click if the mouse is released, there was no dragging, and we were ready to check for a release
        if (Input.GetMouseButtonUp(0) && !isDragging && readyToCheckRelease)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast the ray from the current mouse position
            LayerMask layerMask = LayerMask.GetMask("raycasts");
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, layerMask))
            {
                // The ray hit something, 'hit.collider.gameObject' is the GameObject that was hit
                GameObject hitObject = hit.collider.gameObject;

                // Now, you can do something with the hitObject
                Debug.Log("Hit: " + hitObject.name);
            }

            // After handling the release, reset everything
            readyToCheckRelease = false;
        }

        // Reset the dragging and release check if the mouse button is up
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            readyToCheckRelease = false;
        }
    }
}
