using UnityEngine;

public class TrackballRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotationAxis;
    private bool _isRotating;
    public bool canRotate = true;
    // The point in space to rotate around
    public GameObject pivotPoint;
    public Quaternion reference_rot;

    private void Start()
    {
        reference_rot = transform.rotation;
    }

    void Update()
    {
        if (canRotate)
        {

            if (Input.GetMouseButtonDown(0))
            {
                // When the left mouse button is pressed, mark the object as rotating
                _isRotating = true;
                // Capture the starting point
                _mouseReference = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                // When the left mouse button is released, stop rotating
                _isRotating = false;
            }

            if (_isRotating)
            {
                // Calculate the offset between the previous and current mouse positions
                _mouseOffset = (Input.mousePosition - _mouseReference);

                // Convert mouse offset into a rotation around the Y axis and an inverse rotation around the X axis
                _rotationAxis = new Vector3(_mouseOffset.y, -_mouseOffset.x, 0).normalized;

                // Calculate the angle of rotation based on the mouse movement distance and rotation speed
                float angle = _mouseOffset.magnitude * rotationSpeed * Time.deltaTime;

                // Apply the rotation around the calculated axis
                Quaternion rotation = Quaternion.AngleAxis(angle, _rotationAxis);

                // Rotate around the pivot point

                transform.RotateAround(pivotPoint.transform.position, _rotationAxis, angle);

                // Update the reference mouse position for the next frame
                _mouseReference = Input.mousePosition;
            }
        }
        else {
            transform.rotation = Quaternion.Lerp(transform.rotation, reference_rot, 0.5f * Time.deltaTime);
        }
        
    }

}
