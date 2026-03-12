using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RetryButton : MonoBehaviour
{
    [SerializeField] Button button;

    public AudioClip se1;
    AudioSource audioSource;

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
        SceneManager.LoadScene("InStage");

        audioSource = GetComponent<AudioSource>(); //Componentを取得
        audioSource.PlayOneShot(se1);

        //Debug.Log("リトライボタンが押された");
    }
}
