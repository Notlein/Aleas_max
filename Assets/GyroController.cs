using UnityEngine;
using UnityEngine.UI;

/**
 * To put on MainCamera
 */

public class GyroController : MonoBehaviour
{
    public bool gyroEnabled;
    public Gyroscope gyro;
    public Slider slider_ext;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        // Check if the device has a gyroscope
        gyroEnabled = SystemInfo.supportsGyroscope;
        slider_ext = GameObject.FindGameObjectWithTag("slider-scene1").GetComponent<Slider>();

        if (gyroEnabled)
        {
            // Enable the gyroscope
            gyro = Input.gyro;
            gyro.enabled = true;
        }
        else
        {
            Debug.LogWarning("Gyroscope is not supported on this device.");
        }
    }

    private void Update()
    {
        if (gyroEnabled)
        {

            //Changer l'angle pour 0 degrée point 0 (distorsion), 90 degrés point 100 (clean)

            // Get the gyroscope value from the x-axis (between -1 and 1)
            float gyroscopeValue = gyro.attitude.x;

            // Clamp the gyroscope value to a specific range (e.g., -0.5 to 0.5)
            float clampedValue = Mathf.Clamp(gyroscopeValue, -0.5f, 0f);

            // Map the clamped gyroscope value to the desired range (e.g., 0-100)
            float mappedValue = Remap(clampedValue, -0.5f, 0f, 0f, 100f);

            // Set the slider value based on the mapped value
            slider_ext.value = mappedValue;

            //Debug.Log("Gyroscope Value: " + gyroscopeValue);
            //Debug.Log("Mapped Value: " + mappedValue);
        }
    }

    // Helper function to remap a value from one range to another
    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return Mathf.Lerp(toMin, toMax, Mathf.InverseLerp(fromMin, fromMax, value));
        
    }
}