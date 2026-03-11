using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float power = 0.0f;
    #endregion

    #region プロパティ群
    public float MoveSpeed
    {
        get => moveSpeed;
        private set => moveSpeed = value;
    }

    public float Power
    {
        get => power;
        private set => power = value;
    }

    public float MoveSpeedScale
    {
        get => moveSpeedScale;
        private set => moveSpeedScale = value;
    }

    public float PowerScale
    {
        get => powerScale;
        private set => powerScale = value;
    }
    #endregion

    #region
    private float moveSpeedScale = 1.0f;
    private float powerScale = 1.0f;
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
