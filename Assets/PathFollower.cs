using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold your waypoints
    public Vector3[] scales;      // Array to hold scale values for each waypoint
    public float[] transition_speed;      // Speed at which the object moves
    public float rotSpeed = -5.0f;// Rotation speed
    private int waypointIndex = 0;// Current waypoint index

    private float journeyLength;  // Length of the journey between two waypoints
    private float startTime;      // Time when the movement started
    private bool moving = false;  // Is the object currently moving?
    public Transform target; // Assign the target in the inspector


    private void Start()
    {
        if (waypoints.Length != scales.Length)
        {
            Debug.LogError("Waypoints and scales arrays must be of the same length.");
            return;
        }

        // transform.position = waypoints[0].position; // Start at the first waypoint
        // transform.localScale = scales[0];           // Set the initial scale

        // moving = true;
        // StartSegment();
        // moving = false;
    }

    private void Update()
    {
        // advance to next waypoint
        if (Input.GetKeyDown(KeyCode.X) && !moving && waypointIndex < waypoints.Length - 1)
        {
            moving = true;
            StartSegment();
        }
        // advance to next waypoint

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // select = true
            GetComponentInChildren<TrackballRotation>().canRotate = false;
        }
        // advance to next waypoint

        if (moving)
        {
            MoveAlongPath();

        }
        RotateObject();
    }

    public void SetMoving(bool moving)
    {
        this.moving = moving;
    }

    public void StartSegment()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, waypoints[waypointIndex + 1].position);
    }


    private void MoveAlongPath()
    {
        float distCovered = (Time.time - startTime) * transition_speed[waypointIndex];
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex + 1].position, transition_speed[waypointIndex] * Time.deltaTime);
        transform.localScale = Vector3.Lerp(scales[waypointIndex], scales[waypointIndex + 1], fractionOfJourney);

        if (transform.position == waypoints[waypointIndex + 1].position)
        {
            moving = false;
            waypointIndex++; // Move to the next waypoint
        }
    }

    private void RotateObject()
    {
        if (GetComponentInChildren<TrackballRotation>().canRotate)
        {
            // Rotates the object around the global Y-axis at the specified speed
            transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime, Space.World);
        }
        else
        {

        }

    }
}
