
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [Header("SE 設定")]
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip clickSE;
    [SerializeField, Range(0f, 1f)] private float seVolume = 1.0f;

    [Header("BGM 設定")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField, Range(0f, 1f)] private float bgmVolume = 0.5f;
    [SerializeField] private bool playBgmOnStart = true;

    [Header("画面遷移設定")]
    [SerializeField] private CanvasGroup fadeCanvas;      // 黒フェード用
    [SerializeField] private float fadeDuration = 0.75f;  // 黒くなる秒数
    [SerializeField] private string nextSceneName = "InStage";

    [Header("入力/多重起動ガード")]
    [SerializeField] private bool disableInteractionWhileFading = true;

    [Header("UI 参照")]
    [SerializeField] private Button startButton;

    // ▼ 追加：トランジションアニメを使う場合（任意）
    [Header("トランジション（任意）")]
    [SerializeField] private Animator transitionAnimator;   // 画面遷移用アニメーター
    [SerializeField] private string transitionTrigger = "Out"; // 再生するトリガー
    [SerializeField] private string transitionStateName = "OutState"; // 終了待機で参照するステート名
    [SerializeField] private float transitionMaxWait = 3.0f; // 安全タイムアウト

    private bool firstPush = false;

    public void PlayClickSE()
    {
        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE, seVolume);
        }
    }

    private void Start()
    {
        if (playBgmOnStart && bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.volume = bgmVolume;
            bgmSource.Play();
        }
        if (fadeCanvas != null)
        {
            fadeCanvas.blocksRaycasts = false;
            fadeCanvas.alpha = 0f;
        }
        if (startButton != null)
        {
            EventSystem.current?.SetSelectedGameObject(startButton.gameObject);
        }
    }

    // ButtonのOnClickから呼ぶ
    public void PressStart()
    {
        if (firstPush) return;           // 二重起動防止
        firstPush = true;
        Debug.Log("Press Start");

        // 1) クリックSE
        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE, seVolume);
        }

        // 2) 画面遷移アニメ（任意）
        if (transitionAnimator != null && !string.IsNullOrEmpty(transitionTrigger))
        {
            transitionAnimator.ResetTrigger(transitionTrigger);
            transitionAnimator.SetTrigger(transitionTrigger);
        }

        // 3) フェード＆ロードを開始（←ここだけで遷移を管理）
        StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        // 入力ブロック
        if (fadeCanvas != null && disableInteractionWhileFading)
            fadeCanvas.blocksRaycasts = true;

        // ===== まずは「SE と トランジションアニメ」が終わるまで待つ =====
        float seWait = 0f;
        if (seSource != null && clickSE != null)
        {
            // pitchを考慮してSE長を見積もる（PlayOneShotは戻り値がないため）
            float pitch = Mathf.Approximately(seSource.pitch, 0f) ? 1f : seSource.pitch;
            seWait = clickSE.length / Mathf.Max(pitch, 0.0001f);
        }

        // トランジションアニメの終了待ち（AnimatorとState名が設定されている場合）
       
        if (transitionAnimator != null && !string.IsNullOrEmpty(transitionStateName))
        {
            float t = 0f;
            int layer = 0;
            // ステートに入るまで待つ（最大transitionMaxWait）
            while (t < transitionMaxWait)
            {
                var info = transitionAnimator.GetCurrentAnimatorStateInfo(layer);
                if (info.IsName(transitionStateName)) break;
                t += Time.unscaledDeltaTime;
                yield return null;
            }
            // 入ったら normalizedTime>=1 まで待つ（最大transitionMaxWait）
            t = 0f;
            while (t < transitionMaxWait)
            {
                var info = transitionAnimator.GetCurrentAnimatorStateInfo(layer);
                if (info.IsName(transitionStateName) &&
                    !transitionAnimator.IsInTransition(layer) &&
                    info.normalizedTime >= 1f)
                {
                    break;
                }
                t += Time.unscaledDeltaTime;
                yield return null;
            }
           
        }

        // 追加でSEだけ残っている場合に待つ
        float elapsed = 0f;
        while (elapsed < seWait)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // ===== BGMフェードアウト＋画面フェードアウト =====
        float bgmStartVol = (bgmSource != null) ? bgmSource.volume : 0f;

        float tFade = 0f;
        while (tFade < fadeDuration)
        {
            tFade += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(tFade / fadeDuration);

            if (fadeCanvas != null) fadeCanvas.alpha = p;           // 画面暗転
            if (bgmSource != null) bgmSource.volume = Mathf.Lerp(bgmStartVol, 0f, p); // BGMも下げる

            yield return null;
        }

        // ===== シーン存在チェック =====
        bool canLoad = Application.CanStreamedLevelBeLoaded(nextSceneName);
        if (!canLoad)
        {
            Debug.LogWarning($"[Title] シーン '{nextSceneName}' が Build Settings にありません。");
            // 明転して復帰
            float tBack = 0f;
            const float backDuration = 0.4f;
            while (tBack < backDuration)
            {
                tBack += Time.unscaledDeltaTime;
                float p = Mathf.Clamp01(tBack / backDuration);
                if (fadeCanvas != null) fadeCanvas.alpha = 1f - p;
                yield return null;
            }
            if (fadeCanvas != null) fadeCanvas.blocksRaycasts = false;
            if (bgmSource != null) bgmSource.volume = bgmStartVol;
            firstPush = false; // 失敗したので再入力許可
            yield break;
        }

        // ===== 非同期ロード（フェード完了後に実施） =====
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f) yield return null; // 読み込み完了待ち
        yield return null;                             // 1フレーム猶予

        op.allowSceneActivation = true; // ここで初めてシーン切替
    }

    private void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (!firstPush && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            PressStart();
#else
        if (!firstPush && Input.GetKeyDown(KeyCode.Space))
            PressStart();
#endif
    }
}
