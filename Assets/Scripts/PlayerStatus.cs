using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region メンバ変数群
    private float speed = 0.0f;
    private float power = 0.0f;
    #endregion

    #region プロパティ群
    public float Speed
    {
        get
        {
            return speed;
        }
        private set
        {
            speed = value;
        }
    }

    public float Power
    {
        get
        {
            return power;
        }
        private set
        {
            power = value;
        }
    }
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }
    #endregion
}
