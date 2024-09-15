using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AK.Wwise;
using System.Runtime.InteropServices; // For StructLayout

[System.Serializable]
public class AudioEvent
{
    public AK.Wwise.Event wwiseEvent;

    public float duration; // Duration in seconds (now hidden)
}

public class CrossfadeAudioPlayer : MonoBehaviour
{
    public AudioEvent[] scene_audio_events; // Array of AudioEvent
    public float crossfadeDuration = 5f; // Crossfade duration in seconds

    public List<AudioEvent> eventQueue; // List to store shuffled events
    public int currentEventIndex = -1;
    public uint currentPlayingID = 0;
    public uint nextPlayingID = 0;
    public uint trackVolumeRTPCId;

    void Start()
    {
        if (scene_audio_events == null || scene_audio_events.Length == 0)
        {
            Debug.LogError("No audio events found in scene_audio_events.");
            return;
        }

        // Get the RTPC ID for "TrackVolume"
        trackVolumeRTPCId = AkSoundEngine.GetIDFromString("TrackVolume");

        // For each AudioEvent, get its duration from Wwise
        foreach (var audioEvent in scene_audio_events)
        {
            uint eventID = audioEvent.wwiseEvent.Id;

            AkEventInfo eventInfo = new AkEventInfo();
            

            if (eventInfo != null)
            {
                // Use the maximum duration (in milliseconds)
                
                Debug.Log($"Event {audioEvent.wwiseEvent.Name} duration set to {audioEvent.duration} seconds.");
            }
            else
            {
                Debug.LogError($"Failed to get duration for event {audioEvent.wwiseEvent.Name}. Using default duration.");
                audioEvent.duration = 10f; // Default duration if failed to get info
            }
        }

        eventQueue = new List<AudioEvent>(scene_audio_events);
        ShuffleEvents();
        StartCoroutine(PlayCrossfadedAudio());
    }

    IEnumerator PlayCrossfadedAudio()
    {
        while (true)
        {
            if (currentEventIndex == -1) // First iteration
            {
                currentEventIndex = 0;
                var currentEvent = eventQueue[currentEventIndex];

                currentPlayingID = currentEvent.wwiseEvent.Post(gameObject);

                // Ensure the volume is at 100%
                AkSoundEngine.SetRTPCValueByPlayingID(trackVolumeRTPCId, 100f, currentPlayingID);

                Debug.Log($"Started playing: {currentEvent.wwiseEvent.Name} (ID: {currentPlayingID})");
                Debug.Log(currentEvent.duration);
                // Wait for the duration minus the crossfade time
                yield return new WaitForSeconds(currentEvent.duration - crossfadeDuration);
            }
            else
            {
                var currentEvent = eventQueue[currentEventIndex];
                int nextIndex = (currentEventIndex + 1) % eventQueue.Count;
                var nextEvent = eventQueue[nextIndex];

                // Start the next track with volume at 0%
                nextPlayingID = nextEvent.wwiseEvent.Post(gameObject);
                AkSoundEngine.SetRTPCValueByPlayingID(trackVolumeRTPCId, 0f, nextPlayingID);

                Debug.Log($"Started crossfade to: {nextEvent.wwiseEvent.Name} (ID: {nextPlayingID})");

                // Fade in the next track over crossfadeDuration
                AkSoundEngine.SetRTPCValueByPlayingID(
                    trackVolumeRTPCId,
                    100f,
                    nextPlayingID,
                    (int)(crossfadeDuration * 1000),
                    AkCurveInterpolation.AkCurveInterpolation_Sine
                );

                // Fade out the current track over crossfadeDuration
                AkSoundEngine.SetRTPCValueByPlayingID(
                    trackVolumeRTPCId,
                    0f,
                    currentPlayingID,
                    (int)(crossfadeDuration * 1000),
                    AkCurveInterpolation.AkCurveInterpolation_Sine
                );

                // Wait for the crossfade duration
                yield return new WaitForSeconds(crossfadeDuration);

                // Stop the current track after fade-out
                AkSoundEngine.StopPlayingID(currentPlayingID);

                Debug.Log($"Stopped playing: {currentEvent.wwiseEvent.Name} (ID: {currentPlayingID})");

                // Update indices and IDs for the next iteration
                currentEventIndex = nextIndex;
                currentPlayingID = nextPlayingID;
                Debug.Log(nextEvent.duration);

                // Wait for the next track's duration minus the crossfade time
                yield return new WaitForSeconds(nextEvent.duration - crossfadeDuration);
            }
        }
    }

    void ShuffleEvents()
    {
        for (int i = eventQueue.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioEvent temp = eventQueue[i];
            eventQueue[i] = eventQueue[randomIndex];
            eventQueue[randomIndex] = temp;
        }
        currentEventIndex = -1; // Reset the index after shuffling
    }
}

// Define AkEventInfo and AKRESULT if they are not already defined
[StructLayout(LayoutKind.Sequential)]
public class AkEventInfo
{
    public uint eventID;
    public uint eventType;
    public uint maxDuration; // in milliseconds
    public uint minDuration; // in milliseconds
    [MarshalAs(UnmanagedType.I1)]
    public bool isInfinite;
}

public enum AKRESULT
{
    AK_Success = 1,
    AK_Fail = 0,
    // Add other AKRESULT values as needed
}
