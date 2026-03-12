using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToTitleButton : MonoBehaviour
{
    [SerializeField] Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(() => { Onclick(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Onclick()
    {
        //todo：後でシーンの名前を正式名称に変える
        SceneManager.LoadScene("title");

        //Debug.Log("タイトルへボタンが押された");
    }
}
