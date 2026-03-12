using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float power = 0.0f;
    #endregion

    #region プロパティ群
    public float MoveSpeed
    {
        get => moveSpeed * _moveSpeedScale;
    }

    public float Power
    {
        get => power * _powerScale;
    }

    public float MoveSpeedScale
    {
        get => _moveSpeedScale;
    }

    public float PowerScale
    {
        get => _powerScale;
    }
    #endregion

    #region メンバ変数群
    private float _moveSpeedScale = 1.0f;
    private float _powerScale = 1.0f;
    #endregion

    #region 関数群
    public void OnChangeSpeedScale(float moveSpeedScale)
    {
        _moveSpeedScale = moveSpeedScale;
    }

    public void OnChangePowerScale(float powerScale)
    {
        _powerScale = powerScale;
    }
    #endregion
}
