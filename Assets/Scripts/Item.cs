using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private int cost = 0;
    #endregion

    #region メンバ変数群
    protected int _level = 1;
    #endregion

    #region 関数群
    public void Start()
    {

    }

    public void Update()
    {

    }

    /// <summary>
    /// アイテムが取得された
    /// </summary>
    public abstract void OnAcquired(PlayerStatus status);
    #endregion
}
