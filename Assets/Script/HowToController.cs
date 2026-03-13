using UnityEngine;
using UnityEngine.UI;

public class HowToController : MonoBehaviour
{
    [Header("表示したいパネル（全画面のPanel）")]
    [SerializeField] private GameObject howToPanel;

    [Header("開くボタン")]
    [SerializeField] private Button openButton;

    [Header("閉じるボタン（パネル内）")]
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject howToButtonObject;

    private void Awake()
    {
        // 念のため非表示スタート
        if (howToPanel != null) howToPanel.SetActive(false);

        // クリック時の挙動を登録
        if (openButton != null) openButton.onClick.AddListener(OpenHowTo);
        if (closeButton != null) closeButton.onClick.AddListener(CloseHowTo);



        if (howToButtonObject == null && openButton != null)
            howToButtonObject = openButton.gameObject;

    }

    public void OpenHowTo()
    {
        if (howToPanel != null) howToPanel.SetActive(true);

        if (howToButtonObject != null) howToButtonObject.SetActive(false);
    }

    public void CloseHowTo()
    {
        if (howToPanel != null) howToPanel.SetActive(false);
        if (howToButtonObject != null) howToButtonObject.SetActive(true);
        // Time.timeScale = 1f;  // Openで止めた場合はここで戻す
    }
}