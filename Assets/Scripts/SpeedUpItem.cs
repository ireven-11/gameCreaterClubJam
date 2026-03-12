using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SpeedUpItem : ItemBase
{
    #region シリアライズフィールド群
    [SerializeField] private float speedScale = 0.0f;
    #endregion

    #region 関数群
    public override void OnAcquired(PlayerStatus status)
    {
        speedScale = _level * 0.1f + 1;

        _level = Mathf.Min(_level, maxLevel);
        _level++;

        status.OnChangeSpeedScale(speedScale);

        // コストを増加
        cost += _level;
        // コストテキストを更新
        UpdateCostText();
    }
    #endregion
}
