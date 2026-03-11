using UnityEngine;

public class SpeedUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float speedScale = 0.0f;
    #endregion

    #region プロパティ群
    public float SpeedScale
    {
        get => speedScale;
        private set => speedScale = value;
    }
    #endregion

    #region 関数群
    public override void OnAcquired(PlayerStatus status)
    {
        _level++;
        status.OnChangeSpeedScale(speedScale);
    }
    #endregion
}
