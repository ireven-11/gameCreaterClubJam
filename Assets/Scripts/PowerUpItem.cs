using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PowerUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float powerScale = 0.0f;
    [SerializeField] private int maxLevel = 1;
    #endregion

    #region 関数群
    public override void OnAcquired(PlayerStatus status)
    {
        if(_level >= maxLevel) { return; }

        _level++;
        powerScale = _level;
        _level = Mathf.Min(_level, maxLevel);
        status.OnChangePowerScale(powerScale);

        Debug.Log(powerScale);
    }
    #endregion
}
