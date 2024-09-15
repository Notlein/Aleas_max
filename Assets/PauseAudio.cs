using UnityEngine;
using AK.Wwise; // Import the AK namespace

public class SoundControl : MonoBehaviour
{
    // Reference to the Wwise Event that you want to pause
    public AK.Wwise.Event wwiseEvent;

    public void PauseSound()
    {
        // Trigger the "Paused" state in Wwise
        AkSoundEngine.SetState("SoundStates", "Paused");
    }
}
