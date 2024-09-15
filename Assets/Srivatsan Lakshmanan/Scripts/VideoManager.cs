using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class VideoManager : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public VideoPlayer currentVideoPlayer;
    public VideoPlayer player;
    public VideoPlayer player2;

    public Image progress;
    public SceneVars sceneVars;

    public Image video_button;
    public Sprite pause_sprite;
    public Sprite play_sprite;
    public int delay;
    private Image progressBar;
    private double time;
    private double seek_time;
    private uint audio_id;
    public AK.Wwise.Event MyEvent;
    public bool videoPlay = true;
    public Color newColor;

    private void Start()
    {
        
        currentVideoPlayer = player;
        player.targetCameraAlpha = 0.98f;
        // Assign references
        GameObject.FindGameObjectWithTag("video-progress").GetComponent<Image>().color = sceneVars.scene_color;

        // Shuffle the videos array
        Shuffle(sceneVars.scene_videos);

        // Proceed with the rest of your method
        int vid_len = sceneVars.scene_videos.Length;
        if (vid_len != 0)
        {
            int randomSeed = Mathf.FloorToInt(UnityEngine.Random.value * vid_len);
            player.clip = sceneVars.scene_videos[randomSeed];
            //audio_id = player.GetComponent<AkAmbient>().playingId;
            //Debug.Log(audio_id);
        }


        postEv(delay);

    }
    private async void postEv(int delay) {
        await Task.Delay(delay);
        MyEvent.Post(gameObject);
    }
    private void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }



    void Update()
    {
        if (currentVideoPlayer.frameCount > 0)
        {
            progress.fillAmount = (float)currentVideoPlayer.frame / (float)currentVideoPlayer.frameCount;
        }

        if (progress.fillAmount > 0.99f )
        {
            if (SingletonSceneLoader.Instance != null)
            {
                StartCoroutine(ChangeScene());
            }
        }
        else
        {
            ProcessVideoMismatch();
        }
    }

    // Only used for Scene 4
    private void ProcessVideoMismatch()
    {
        if (progress.fillAmount > 0.90f)
        {
            if (currentVideoPlayer == player)
            {
                ProposeNewVideoClip(player2);
                StartCoroutine(VideoFadeInFadeOut(player, player2));
            }
            else
            {
                ProposeNewVideoClip(player);
                StartCoroutine(VideoFadeInFadeOut(player2, player));
            }

            SwitchVideoPlayer();
        }
    }

    private IEnumerator VideoFadeInFadeOut(VideoPlayer p1, VideoPlayer p2)
    {
        p1.targetCameraAlpha = 1.0f;
        p2.targetCameraAlpha = 0f;

        p2.Play();
        float time = 0;
        float duration = 10f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / duration);

            p1.targetCameraAlpha = 1f - blend;
            p2.targetCameraAlpha = blend;

            yield return null;
        }
        // p1.Pause();
    }

    private void ProposeNewVideoClip(VideoPlayer p)
    {
        p.clip = sceneVars.scene_videos[UnityEngine.Random.Range(0, sceneVars.scene_videos.Length)];
    }

    private void SwitchVideoPlayer()
    {
        if (currentVideoPlayer == player)
        {
            currentVideoPlayer = player2;
        }
        else
        {
            currentVideoPlayer = player;
        }
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        SingletonSceneLoader.Instance.PrepareNextScene(0);
    }


    public void OnDrag(PointerEventData eventData)
    {
        //TrySkip(eventData);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip(eventData);
    }
    private void SkipToPercent(float pct)
    {

        var frame = currentVideoPlayer.frameCount * pct;
        currentVideoPlayer.frame = (long)frame;
        // objetWise =  functionWise(pct)
        //Debug.Log(player.time);
        AkSoundEngine.SeekOnEvent(MyEvent.Id, gameObject, pct);
    }
    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
            Debug.Log(pct);
        }
    }
    public void VideoPlayerPause()
    {
        if (videoPlay)
        {
            if (currentVideoPlayer != null)
            {
                MyEvent.ExecuteAction(gameObject, AkActionOnEventType.AkActionOnEventType_Pause, 0, AkCurveInterpolation.AkCurveInterpolation_Constant);
                currentVideoPlayer.Pause();
            }


            newColor = video_button.color;
            newColor.a = 0.7f;
            video_button.color = newColor;
            videoPlay = false;
        }
        else
        {
            VideoPlayerPlay();


        }



    }
    public void VideoPlayerPlay()
    {
        if (player != null)
        {
            MyEvent.ExecuteAction(gameObject, AkActionOnEventType.AkActionOnEventType_Resume, 0, AkCurveInterpolation.AkCurveInterpolation_Constant);
            player.Play();
        }
        newColor = video_button.color;
        newColor.a = 0.0f;
        video_button.color = newColor;
        videoPlay = true;

    }

}

