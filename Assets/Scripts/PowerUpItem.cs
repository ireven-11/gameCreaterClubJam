using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PowerUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float powerScale = 0.0f;
    #endregion

    #region 関数群
    public override void OnAcquired(PlayerStatus status)
    {
        _level++;
        _level = Mathf.Min(_level, maxLevel);

        powerScale = _level;
        status.OnChangePowerScale(powerScale);

        // コストを増加
        cost += _level;
    }
    #endregion
}
