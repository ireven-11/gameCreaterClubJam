using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region プロパティ群
    public bool IsMove
    {
        get => _inputDir.magnitude > 0;
    }

    public Vector2 MoveDir
    {
        get => _moveDir;
        private set => _moveDir = value;
    }
    #endregion

    #region メンバ変数群
    private Animator _animator = null;
    private Vector2 _moveDir = Vector2.zero;
    private Vector2 _inputDir = Vector2.zero;
    private PlayerStatus _status = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _animator = GetComponent<Animator>();
        _status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        _inputDir = Vector2.zero;

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
        Debug.Log(_moveDir);
        var addPos = _moveDir * _status.Speed * Time.deltaTime;
        transform.position += new Vector3(addPos.x, addPos.y, 0.0f);
    }

    private void Attack()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("Attack");
        }
    }
    #endregion
}
