using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    private Image _sceneFadeImage;

    private void Awake()
    {
        _sceneFadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeOutCoroutine(float fadeTime, Color fadeColor)
    {
        gameObject.SetActive(true);

        Color startColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0);
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        yield return FadeCoroutine(fadeTime, startColor, targetColor);
    }

    public IEnumerator FadeInCoroutine(float fadeTime, Color fadeColor)
    {
        gameObject.SetActive(true);

        Color startColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        yield return FadeCoroutine(fadeTime, startColor, targetColor);

        gameObject.SetActive(false);
    }

    private IEnumerator FadeCoroutine(float fadeTime, Color startColor, Color targetColor)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            elapsedPercentage = elapsedTime / fadeTime;

            _sceneFadeImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);
        }
    }
}
