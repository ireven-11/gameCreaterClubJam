using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToTitleButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] private Fade fade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(OnClick);
        fade.OnFadeOutComplete += OnFadeOutComplete;
    }

    // Update is called once per frame
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
            //Debug.LogError("Fade がインスペクタで設定されていません");
            return;
        }

        //Debug.Log("RetryButton: StartFadeOut 呼び出し");
        fade.StartFadeOut();
    }

    private void OnFadeOutComplete()
    {
        //Debug.Log("RetryButton: フェードアウト完了 → シーン遷移");
        SceneManager.LoadScene("title");

        //スコア初期化
        ScoreManager.Instance.score = 0;
    }
}
