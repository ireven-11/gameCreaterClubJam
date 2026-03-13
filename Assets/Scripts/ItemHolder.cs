using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ItemHolder : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private TextMeshProUGUI wood;
    [SerializeField] private TextMeshProUGUI power;
    [SerializeField] private TextMeshProUGUI speed;
    #endregion

    #region メンバ変数群
    private Dictionary<string, ItemBase> _items = new Dictionary<string, ItemBase>();
    private int _woodNum = 0;
    #endregion

    #region 関数群
    /// <summary>
    /// 取得したアイテムを追加 (上書き)
    /// </summary>
    /// <param name="item">追加するアイテム</param>
    public void AddItem(ItemBase item)
    {
        _items[item.name] = item;
        UpdateItemLevelText();
    }

    /// <summary>
    /// 特定のアイテムを取得する
    /// </summary>
    /// <param name="name">取得するアイテムの名前</param>
    /// <returns>取得するアイテム (存在しなければnull)</returns>
    public ItemBase GetItem(string name)
    {
        if (_items.TryGetValue(name, out ItemBase item))
        {
            return item;
        }

        return null;
    }

    /// <summary>
    /// 木材を消費する
    /// </summary>
    /// <param name="cost">消費コスト</param>
    /// <returns>true : 木材が足りた, false : 足りなかった</returns>
    public bool ConsumeWood(int cost)
    {
        if(_woodNum < cost) { return false; }

        _woodNum -= cost;
        UpdateWoodText();
        return true;
    }

    public void AddWood(int wood)
    {
        _woodNum += wood;
        UpdateWoodText();
    }

    public void UpdateWoodText()
    {
        wood.text = _woodNum.ToString("D6");
    }

    public void UpdateItemLevelText()
    {
        if (_items.TryGetValue("SpeedUpItem", out ItemBase speedUpItem))
        {
            speed.text = "Lv " + speedUpItem.Level.ToString();
        }
        if (_items.TryGetValue("PowerUpItem", out ItemBase powerUpItem))
        {
            power.text = "Lv " + powerUpItem.Level.ToString();
        }
    }
    #endregion
}
