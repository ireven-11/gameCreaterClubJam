using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeTexts;
    float totalTime = 60.0f;
    int retime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //§ŒÀŠÔ‚ğŒ¸‚ç‚·ˆ—
        totalTime       -= Time.deltaTime;
        retime          = (int)totalTime;
        timeTexts.text  = retime.ToString();

        //c‚èŠÔ‚ª0‚É‚È‚Á‚½‚çƒV[ƒ“‘JˆÚ‚Ìˆ—‚ğ‚·‚é
        if(retime <= 0)
        {
            SceneManager.LoadScene("result");
        }
    }
}
