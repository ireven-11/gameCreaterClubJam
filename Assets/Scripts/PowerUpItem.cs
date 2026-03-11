using UnityEngine;

public class PowerUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float powerScale = 0.0f;
    #endregion

    #region プロパティ群
    public float PowerScale
    {
        get => powerScale;
        private set => powerScale = value;
    }
    #endregion

    #region 関数群
    public override void OnAcquired(PlayerStatus status)
    {
        _level++;
        status.OnChangePowerScale(powerScale);
    }
    #endregion
}
