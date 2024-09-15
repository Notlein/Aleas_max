using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AK.Wwise;

public class SceneVars : MonoBehaviour
{
    public VideoClip[] scene_videos;
    public AK.Wwise.Event[] scene_audio; // Wwise Events array

    public Color scene_color;
    void Start()
    {

    }
    private void Awake()
    {
        //AkSoundEngine.Init(
        //    new AkInitializationSettings
        //    {
        //        // Set your initialization parameters here
        //        // For example, you can set the language, the default pool size, etc.
        //        // Refer to the Wwise documentation for the available settings.
        //    });
    }

    public void PlayRandomAudio()
    {

    }
}
