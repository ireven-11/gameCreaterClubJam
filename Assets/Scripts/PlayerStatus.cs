using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float power = 0.0f;
    #endregion

    #region プロパティ群
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }

    public float Power
    {
        get => power;
        private set => power = value;
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
