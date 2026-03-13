using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _textMeshProUGUI.text = ScoreManager.Instance.score.ToString("D6");
    }
}
