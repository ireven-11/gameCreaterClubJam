using TMPro;
using UnityEngine;

public class ClearRank : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //todo：スコアの仕組みができたらスコアのゲッターを呼び出して代入する
        score = ScoreManager.Instance.score;

        if (score >= 100)
        {
            rankText.text = string.Format("<color=red><size=200%>全宇宙</size></color>") + rankText.text;
        }
        else if (score >= 50)
        {
            rankText.text = string.Format("<color=red><size=200%>地球中</size></color>") + rankText.text;
        }
        else if (score >= 40)
        {
            rankText.text = string.Format("<color=red><size=200%>日本中</size></color>") + rankText.text;
        }
        else if (score >= 30)
        {
            rankText.text = string.Format("<color=red><size=200%>県中</size></color>") + rankText.text;
        }
        else if (score >= 20)
        {
            rankText.text = string.Format("<color=red><size=200%>町中</size></color>") + rankText.text;
        }
        else if (score >= 10)
        {
            rankText.text = string.Format("<color=red><size=200%>村中</size></color>") + rankText.text;
        }
        else
        {
            rankText.text = string.Format("<color=red><size=200%>近所</size></color>") + rankText.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
