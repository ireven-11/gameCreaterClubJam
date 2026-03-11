using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region プロパティ群
    public bool IsRun
    {
        get => _moveDir.magnitude > 0;
    }

    public bool IsAttack
    {
        get => Keyboard.current.spaceKey.wasPressedThisFrame;
    }
    
    public Vector2 MoveDir
    {
        get => _moveDir;
        private set => _moveDir = value;
    }
    #endregion

    #region メンバ変数群
    private Vector2 _moveDir = Vector2.zero;
    private PlayerStatus _status = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
    }

    private void Move()
    {
        var _inputDir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            _inputDir.y += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            _inputDir.y -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            _inputDir.x += 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            _inputDir.x -= 1;
        }
        _inputDir.Normalize();

        _moveDir = Vector2.Lerp(_moveDir, _inputDir, 0.5f);
        var addPos = _moveDir * _status.MoveSpeed * Time.deltaTime;
        transform.position += new Vector3(addPos.x, addPos.y, 0.0f);
    }
    #endregion
}
