using UnityEngine;

public class DropItem : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float jumpHeight = 3f;     // 跳ねる高さ
    [SerializeField] private float speed = 3f;           // 落下速度
    [SerializeField] private float horizontalRange = 0.5f; // 木の下で少し横にずらす
    [SerializeField] private float dropDistance = 2f;   // 木から落ちる距離
    #endregion

    #region プロパティ群
    public bool IsDropping
    {
        get => _isDropping;
    }
    #endregion

    #region メンバ変数群
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _height;      // 上下の弧の高さ
    private bool _isDropping = false;
    private float _t;           // 補間パラメータ 0→1
    #endregion

    #region 関数群
    // 木からドロップするときに呼ぶ
    public void DropFromTree(Vector3 treePosition)
    {
        float xOffset = Random.Range(-horizontalRange, horizontalRange);

        _startPos = treePosition;
        _targetPos = new Vector3(treePosition.x + xOffset, treePosition.y - dropDistance, treePosition.z);

        _height = jumpHeight;
        _t = 0f;
        transform.position = _startPos;
        _isDropping = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDropping)
        {
            _t += speed * Time.deltaTime;

            // 線形補間でX/Yの位置
            Vector3 pos = Vector3.Lerp(_startPos, _targetPos, _t);

            // 上下の弧（ジャンプの高さ）を追加
            float arc = Mathf.Sin(_t * Mathf.PI) * _height;
            pos.y += arc;

            transform.position = pos;

            if (_t >= 1f)
            {
                transform.position = _targetPos;
                _isDropping = false;
            }
        }
    }
    #endregion
}
