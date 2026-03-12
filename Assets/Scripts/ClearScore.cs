using TMPro;
using UnityEngine;

public class ClearScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //todo：スコアの仕組みができたらスコアのゲッターを呼び出して代入する
        score = 100;

        scoreText.text = string.Format("<color=red><size=200%>{0}</size></color>", score) + scoreText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
