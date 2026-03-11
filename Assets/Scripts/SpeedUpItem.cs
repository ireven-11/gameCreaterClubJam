using UnityEngine;

public class SpeedUpItem : Item
{
    #region シリアライズフィールド群
    [SerializeField] private float speedUp = 0.0f;
    #endregion

    #region プロパティ群
    public float SpeedUp
    {
        get => speedUp;
        private set => speedUp = value;
    }
    #endregion

    #region 関数群
    #endregion
}
