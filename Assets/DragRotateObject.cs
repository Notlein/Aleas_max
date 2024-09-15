using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotateObject : MonoBehaviour
{
    private bool isTouched = false;
    private Vector2 touchStartPos;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.touchCount ==1)
        {
            Touch touch = Input.GetTouch(0);
            



            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if the touch started on the object
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>())
                    {
                        isTouched = true;
                        touchStartPos = touch.position;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isTouched)
                    {
                        Vector2 touchDelta = touch.position - touchStartPos;

                        // Calculate rotation amounts based on touch movement
                        float rotationSpeed = 0.5f; // Adjust as needed
                        float rotationAmountX = -touchDelta.x * rotationSpeed;
                        // absolute function to prevent negative
                        float rotationAmountY = touchDelta.y * rotationSpeed;

                        // Apply rotations
                        Quaternion rotationX = Quaternion.Euler(0, rotationAmountX, 0);
                        Quaternion rotationY = Quaternion.Euler(rotationAmountY, 0, 0);

                        // Combine rotations while keeping the original position
                        transform.rotation = rotationX * initialRotation * rotationY;
                    }
                    break;

                case TouchPhase.Ended: initialRotation = transform.rotation;break;
                case TouchPhase.Canceled:
                    isTouched = false;
                    break;
            }
            
        }
        else if (Input.touchCount > 1)
        {
            Debug.Log("multi input!");
            

        }
    }
}
