using Fungus;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransition : MonoBehaviour
{
    public Image img;
    public float fadeDuration = 2f;

    // Fade Out (hilang)
    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    // Fade In (muncul)
    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;

        Color c = img.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            // Lerp alpha
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            img.color = c;

            yield return null;
        }

        // Pastikan alpha tepat di akhir
        c.a = endAlpha;
        img.color = c;
    }
}
