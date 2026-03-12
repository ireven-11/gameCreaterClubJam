//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class RetryButton : MonoBehaviour
//{
//    [SerializeField] private Button button;
//    [SerializeField] private Fade fade;   // フェード用スクリプトへの参照

//    private void Start()
//    {
//        button.onClick.AddListener(OnClick);

//        // フェードアウト完了時にシーン遷移するよう登録
//        fade.OnFadeOutComplete += OnFadeOutComplete;
//    }

//    private void OnDestroy()
//    {
//        // 念のため解除（シーン跨ぎで残らないように）
//        if (fade != null)
//        {
//            fade.OnFadeOutComplete -= OnFadeOutComplete;
//        }
//    }

//    private void OnClick()
//    {
//        // まずフェードアウト開始（この時点ではシーン遷移しない）
//        fade.StartFadeOut();

//        //Debug.Log("リトライボタンが押された");
//    }

//    private void OnFadeOutComplete()
//    {
//        //Debug.Log("しーんせんいします");

//        // フェードアウトが終わったタイミングでシーン遷移
//        SceneManager.LoadScene("InStage");
//    }
//}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Fade fade;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
        fade.OnFadeOutComplete += OnFadeOutComplete;
    }

    private void OnDestroy()
    {
        if (fade != null)
        {
            fade.OnFadeOutComplete -= OnFadeOutComplete;
        }
    }

    private void OnClick()
    {
        if (fade == null)
        {
            Debug.LogError("Fade がインスペクタで設定されていません");
            return;
        }

        Debug.Log("RetryButton: StartFadeOut 呼び出し");
        fade.StartFadeOut();
    }

    private void OnFadeOutComplete()
    {
        Debug.Log("RetryButton: フェードアウト完了 → シーン遷移");
        SceneManager.LoadScene("InStage");
    }
}
