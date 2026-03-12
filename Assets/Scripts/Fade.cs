//using UnityEngine;
//using UnityEngine.UI;
//public class Fade : MonoBehaviour
//{
//    [SerializeField] private Image fadePanel;
//    [SerializeField] private float fadeTime;

//    private float   fadeAlpha   = 0.0f;
//    private bool    isFadeIn    = false;
//    private bool    isFadeOut   = false;


//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(isFadeIn)
//        {
//            FadeIn();
//        }
//        else if(isFadeOut)
//        {
//            FadeOut();
//        }
//    }

//    private void FadeIn()
//    {
//        fadeAlpha -= Time.deltaTime / fadeTime;

//        //フェードイン終了
//        if(fadeAlpha <= 0.0f)
//        {
//            fadeAlpha   = 0.0f;
//            isFadeIn    = false;
//        }

//        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, fadeAlpha);
//    }

//    private void FadeOut()
//    {
//        fadeAlpha += Time.deltaTime / fadeTime;

//        //フェードイン終了
//        if (fadeAlpha >= 1.0f)
//        {
//            fadeAlpha = 1.0f;
//            isFadeOut = false;
//        }

//        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, fadeAlpha);
//    }

//    public void StartFadeIn()
//    {
//        if (isFadeIn) return;

//        fadeAlpha   = 1.0f;
//        isFadeIn    = true;
//        isFadeOut   = false;
//    }

//    public void StartFadeOut()
//    {
//        if (isFadeOut) return;

//        fadeAlpha   = 0.0f;
//        isFadeIn    = false;
//        isFadeOut   = true;
//    }
//}

//using System;
//using UnityEngine;
//using UnityEngine.UI;

//public class Fade : MonoBehaviour
//{
//    [SerializeField] private Image fadePanel;
//    [SerializeField] private float fadeTime;

//    private float fadeAlpha = 0.0f;
//    private bool isFadeIn = false;
//    private bool isFadeOut = false;

//    public event Action OnFadeInComplete;   // フェードアウト完了イベント
//    public event Action OnFadeOutComplete;   // フェードアウト完了イベント

//    void Update()
//    {
//        if (isFadeIn)
//        {
//            FadeIn();
//        }
//        else if (isFadeOut)
//        {
//            FadeOut();
//        }
//    }

//    private void FadeIn()
//    {
//        fadeAlpha -= Time.deltaTime / fadeTime;

//        if (fadeAlpha <= 0.0f)
//        {
//            fadeAlpha = 0.0f;
//            isFadeIn = false;

//            // ここで「フェードイン終わったよ」と通知
//            OnFadeInComplete?.Invoke();
//        }

//        fadePanel.color = new Color(
//            fadePanel.color.r,
//            fadePanel.color.g,
//            fadePanel.color.b,
//            fadeAlpha
//        );
//    }

//    private void FadeOut()
//    {
//        fadeAlpha += Time.deltaTime / fadeTime;

//        if (fadeAlpha >= 1.0f)
//        {
//            fadeAlpha = 1.0f;
//            isFadeOut = false;

//            // ここで「フェードアウト終わったよ」と通知
//            OnFadeOutComplete?.Invoke();
//        }

//        fadePanel.color = new Color(
//            fadePanel.color.r,
//            fadePanel.color.g,
//            fadePanel.color.b,
//            fadeAlpha
//        );
//    }

//    public void StartFadeIn()
//    {
//        if (isFadeIn) return;

//        fadeAlpha = 1.0f;
//        isFadeIn = true;
//        isFadeOut = false;
//    }

//    public void StartFadeOut()
//    {
//        if (isFadeOut) return;

//        fadeAlpha = 0.0f;
//        isFadeIn = false;
//        isFadeOut = true;
//    }
//}

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
        // 起動時は真っ黒にしておきたいなら 1.0f にする
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
