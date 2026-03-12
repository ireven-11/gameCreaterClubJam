using UnityEngine;

public class TargetChaser : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float initialVelocity = 1.0f;
    [SerializeField] private float acceleration = 1.0f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private float endChaseDistance = 0.1f;
    #endregion

    #region プロパティ群
    public bool HasReachedTarget
    {
        get => _hasReachedTarget;
    }
    #endregion

    #region メンバ変数群
    private Transform _targetTransform = null;
    private bool _isChasing = false;
    private bool _hasReachedTarget = false;
    private float _moveSpeed = 0.0f;
    private ItemDropper _dropItem = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _dropItem = GetComponent<ItemDropper>();

        _moveSpeed = initialVelocity;
    }

    // Update is called once per frame
    public void Update()
    {
        Chase();
    }

    public void StartChase(Transform targetTransform)
    {
        this._targetTransform = targetTransform;
        _isChasing = true;
    }

    private void Chase()
    {
        if (_dropItem.IsDropping) { return; }
        if (!_isChasing) { return; }
        if (_hasReachedTarget) { return; }

        var dir = (_targetTransform.position - transform.position).normalized;
        _moveSpeed += acceleration;
        transform.position += dir * Mathf.Min(_moveSpeed * Time.deltaTime, maxSpeed);

        // 対象との距離が一定まで近づいた場合、追尾を終了する
        if ((_targetTransform.position - transform.position).magnitude < endChaseDistance)
        {
            _isChasing = false;
            _hasReachedTarget = true;
        }
    }
    #endregion
}
