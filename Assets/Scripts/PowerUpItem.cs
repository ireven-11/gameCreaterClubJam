using UnityEngine;

public class PowerUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float powerUp = 0.0f;
    #endregion

    #region プロパティ群
    public float PowerUp
    {
        get => powerUp;
        private set => powerUp = value;
    }
    #endregion

    #region 関数群
    #endregion
}
