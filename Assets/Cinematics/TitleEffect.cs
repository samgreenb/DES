using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleEffect : IceStatueEffect
{
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float fadeInDuration;
    [SerializeField]
    private float fadeOutDuration;
    [SerializeField]
    private float displayDuration;

    [SerializeField]
    private TMP_Text a;

    public override void Activate()
    {
        StartCoroutine(nameof(FadeIn));
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(startDelay);
        // fadein loop
        float t = 0.0f;
        while (t <= fadeInDuration)
        {
            a.alpha = Mathf.Lerp(0.0f, 1.0f, t / fadeInDuration);
            yield return null;
            t += Time.deltaTime;
        }
        a.alpha = 1.0f;
        yield return new WaitForSeconds(displayDuration);
        StartCoroutine(nameof(FadeOut));
    }

    private IEnumerator FadeOut() {
        // fadeout loop
        float t = 0.0f;
        while (t <= fadeOutDuration)
        {
            a.alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeOutDuration);
            yield return null;
            t += Time.deltaTime;
        }
        a.alpha = 0.0f;
    }
}
