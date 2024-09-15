using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    
    public VideoPlayer videoPlayer;
    public Slider trackingSlider;
   

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        // Assign references
        
        
        //trackingSlider = GameObject.FindGameObjectWithTag("vid_player").GetComponent<Slider>();
    }
    private void Update()
    {
        // Update the slider value based on video progress
        trackingSlider.value = (float)(videoPlayer.time / videoPlayer.clip.length);
    }

    public void OnSliderValueChanged(float value)
    {
        
        // Update video playback based on slider value
        videoPlayer.time = videoPlayer.clip.length * value;
        // Debug.Log(videoPlayer.time);
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

}
