using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    [Tooltip("画面全体を覆う黒画像を持つ CanvasGroup。Alpha=0 から開始すること。")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField, Tooltip("フェードアウトにかける秒数")]
    private float fadeDuration = 0.75f;
    [SerializeField, Tooltip("ロードするシーン名")]
    private string nextSceneName = "Game";

    [Header("入力/多重起動ガード")]
    [SerializeField]
    private bool disableInteractionWhileFading = true;

    [Header("UI 参照")]
    [SerializeField] private Button startButton;


    private bool firstPush = false;


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
            fadeCanvas.blocksRaycasts = false; // 初期はクリック可能
            fadeCanvas.alpha = 0f;
        }

        if (startButton != null)
        {
            EventSystem.current?.SetSelectedGameObject(startButton.gameObject);
        }


    }



    // Button の OnClick から呼ぶ
    public void PressStart()
    {
        Debug.Log("Press Start");



        // 1) まず SE を鳴らす

        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE, seVolume);
        }

      

        // 2) 二重押し防止しつつ、以降の処理へ
        if (!firstPush)
        {
            Debug.Log("GoNext");
            firstPush = true;

            //SceneManager.LoadScene("Game");
            //audioSource.Stop();
            // ここでシーン遷移やアニメ開始などを行う
            // e.g., SceneManager.LoadScene("Game");
        }

        StartCoroutine(FadeAndLoad());
    }


    private IEnumerator FadeAndLoad()
    {
        // 入力ブロック
        if (fadeCanvas != null && disableInteractionWhileFading)
        {
            fadeCanvas.blocksRaycasts = true; // 以降のクリックを遮断
        }

        // BGM をフェードアウト（任意）
        float bgmStartVol = (bgmSource != null) ? bgmSource.volume : 0f;

        // ===== フェードアウト（暗転） =====
        float tFade = 0f;
        while (tFade < fadeDuration)
        {
            tFade += Time.unscaledDeltaTime; // 時間停止の影響を受けない
            float p = Mathf.Clamp01(tFade / fadeDuration);

            if (fadeCanvas != null)
            {
                fadeCanvas.alpha = p; // 画面を徐々に暗く
            }
            if (bgmSource != null)
            {
                bgmSource.volume = Mathf.Lerp(bgmStartVol, 0f, p);
            }
            yield return null;
        }

        // ===== シーン存在チェック（まだ Game が無い/ビルド未追加なら戻す） =====
        bool canLoad = Application.CanStreamedLevelBeLoaded(nextSceneName);
        if (!canLoad)
        {
            Debug.LogWarning($"[Title] シーン '{nextSceneName}' は Build に含まれていません。ロードをスキップしてタイトルに戻します。");

            // 少し間を置く（好みで調整）
            yield return new WaitForSecondsRealtime(0.3f);

            // 画面をフェードバック（明転）
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

            // BGM を元の音量に戻す（必要なら）
            if (bgmSource != null) bgmSource.volume = bgmStartVol;

            yield break;
        }

        // ===== 非同期ロード =====
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false; // 読み込み完了まで暗転維持

        while (op.progress < 0.9f) yield return null;
        yield return null;

        op.allowSceneActivation = true; // シーン切り替え
    }


    private void Update()
    {

#if ENABLE_INPUT_SYSTEM
        // 新 Input System
        if (!firstPush && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PressStart();
        }
#else
    // 旧 Input Manager
    if (!firstPush && Input.GetKeyDown(KeyCode.Space))
    {
        PressStart();
    }
#endif
    }




    void Reset()
    {
        // 自動取得（任意）：同じGameObjectにAudioSourceが2つある前提
        var sources = GetComponents<AudioSource>();
        if (sources.Length > 0 && seSource == null) seSource = sources[0];
        if (sources.Length > 1 && bgmSource == null) bgmSource = sources[1];
    }


}
