using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SpeedUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float speedScale = 0.0f;
    [SerializeField] private int maxLevel = 1;
    #endregion

    #region 関数群
    public override void OnAcquired(PlayerStatus status)
    {
        if(_level > maxLevel) { return; }

        speedScale = _level * 0.1f + 1;
        _level = Mathf.Min(_level, maxLevel);
        status.OnChangeSpeedScale(speedScale);
        _level++;
    }
    #endregion
}
