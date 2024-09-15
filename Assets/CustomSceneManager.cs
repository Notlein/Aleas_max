using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CustomSceneManager : MonoBehaviour
{
    public Scene sceneToBeLoaded;
    // Start is called before the first frame update
    private int requiredTouches = 2;
    private float maxTouchDistance = 50f; // Adjust as needed
    private float maxTapDuration = 0.5f; // Adjust as needed

    private bool isDetectingMultiTouch = false;
    private float touchStartTime = 0f;
    private Vector2[] touchPositions;

    void Update()
    {
        if (Input.touchCount == requiredTouches)
        {
            if (!isDetectingMultiTouch)
            {
                isDetectingMultiTouch = true;
                touchStartTime = Time.time;
                touchPositions = new Vector2[requiredTouches];
            }

            for (int i = 0; i < requiredTouches; i++)
            {
                Touch touch = Input.GetTouch(i);
                touchPositions[i] = touch.position;
            }

            float touchDuration = Time.time - touchStartTime;
            float touchDistance = Vector2.Distance(touchPositions[0], touchPositions[1]);

            if (touchDuration <= maxTapDuration && touchDistance <= maxTouchDistance)
            {
                // Call your function here
                HandleMultiTouch();
            }
        }
        else
        {
            isDetectingMultiTouch = false;
        }
    }

    void HandleMultiTouch()
    {
        // Implement your desired functionality here
        Debug.Log("Multi-touch gesture detected!");
    }


}
