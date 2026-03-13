using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    float totalTime = 60.0f;
    public int limitTime {  get; private set; } //§ŒÀŠÔ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //§ŒÀŠÔ‚ğŒ¸‚ç‚·ˆ—
        totalTime       -= Time.deltaTime;
        limitTime = (int)totalTime;
        textMeshProUGUI.text  = limitTime.ToString();

        //c‚èŠÔ‚ª0‚É‚È‚Á‚½‚çƒV[ƒ“‘JˆÚ‚Ìˆ—‚ğ‚·‚é
        if(limitTime <= 0)
        {
            SceneManager.LoadScene("result");
        }
    }
}
