using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public abstract class ItemBase : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] protected int cost = 0;
    [SerializeField] protected int maxLevel = 1;
    [SerializeField] protected TextMeshPro costText = null;
    #endregion

    #region プロパティ群
    public int Cost
    {
        get => cost;
    }

    public bool CanAcquired
    {
        get => _level < maxLevel;
    }

    public int Level
    {
        get => _level;
    }
    #endregion

    #region メンバ変数群
    protected int _level = 1;
    #endregion

    #region 関数群
    /// <summary>
    /// アイテムが取得された
    /// </summary>
    public abstract void OnAcquired(PlayerStatus status);

    protected void UpdateCostText()
    {
        costText.text = "Cost " + cost.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        UpdateCostText();
    }
    #endregion
}
