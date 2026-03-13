using System;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeTime = 1f;

    private float fadeAlpha = 0.0f;
    private bool isFadeIn = false;
    private bool isFadeOut = false;

    public event Action OnFadeOutComplete;

    private void Start()
    {
        // ãNìÆéûÇÕê^Ç¡çïÇ…ÇµÇƒÇ®Ç´ÇΩÇ¢Ç»ÇÁ 1.0f Ç…Ç∑ÇÈ
        fadeAlpha = 0f;
        SetAlpha(fadeAlpha);
    }

    private void Update()
    {
        if (isFadeIn)
        {
            FadeIn();
        }
        else if (isFadeOut)
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        fadeAlpha -= Time.deltaTime / fadeTime;

        if (fadeAlpha <= 0.0f)
        {
            fadeAlpha = 0.0f;
            isFadeIn = false;
        }

        SetAlpha(fadeAlpha);
    }

    private void FadeOut()
    {
        fadeAlpha += Time.deltaTime / fadeTime;

        if (fadeAlpha >= 1.0f)
        {
            fadeAlpha = 1.0f;
            isFadeOut = false;
            OnFadeOutComplete?.Invoke();
        }

        SetAlpha(fadeAlpha);
    }

    private void SetAlpha(float alpha)
    {
        var c = fadePanel.color;
        c.a = alpha;
        fadePanel.color = c;
    }

    public void StartFadeIn()
    {
        if (isFadeIn) return;

        fadeAlpha = 1.0f;
        isFadeIn = true;
        isFadeOut = false;
    }

    public void StartFadeOut()
    {
        if (isFadeOut) return;

        fadeAlpha = 0.0f;
        isFadeIn = false;
        isFadeOut = true;
    }
}
