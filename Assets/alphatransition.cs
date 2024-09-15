using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaTransition : MonoBehaviour
{
    public Image image;
    public float transitionDuration = 3.0f;

    private void Start()
    {
        // Optional: You can set the initial alpha value of the image here if needed.
        FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeImage(image.color.a, 1.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeImage(image.color.a, 0.0f));
    }

    private IEnumerator FadeImage(float startAlpha, float targetAlpha)
    {
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        float startTime = Time.time;

        while (Time.time - startTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            image.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        image.color = targetColor; // Ensure the final alpha value is exactly as specified
    }
}
