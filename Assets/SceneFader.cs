using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed;

    private void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    public IEnumerator FadeFromBlack()
    {
        fadeImage.color = new Color(0, 0, 0, 1);
        while (fadeImage.color.a > 0f)
        {
            float fadeAmount = fadeImage.color.a - (fadeSpeed * Time.deltaTime);
            fadeImage.color = new Color(0, 0, 0, fadeAmount);
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        while (fadeImage.color.a < 1f)
        {
            float fadeAmount = fadeImage.color.a + (fadeSpeed * Time.deltaTime);
            fadeImage.color = new Color(0, 0, 0, fadeAmount);
            yield return null;
        }
    }

    // Example of usage
    public void TriggerFadeOut()
    {
        StartCoroutine(FadeToBlack());
    }
}
