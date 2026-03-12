using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHolder : MonoBehaviour
{
    #region メンバ変数群
    private Dictionary<string, Item> _items = new Dictionary<string, Item>();
    int _woodNum = 0;
    #endregion

    #region 関数群
    /// <summary>
    /// 取得したアイテムを追加 (上書き)
    /// </summary>
    /// <param name="item">追加するアイテム</param>
    public void AddItem(Item item)
    {
        _items[item.name] = item;
    }

    /// <summary>
    /// 特定のアイテムを取得する
    /// </summary>
    /// <param name="name">取得するアイテムの名前</param>
    /// <returns>取得するアイテム (存在しなければnull)</returns>
    public Item GetItem(string name)
    {
        if (_items.TryGetValue(name, out Item item))
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

        return true;
    }
    #endregion
}
