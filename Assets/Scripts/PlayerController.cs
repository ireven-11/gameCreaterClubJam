using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private PlayerStatus status = new PlayerStatus();
    #endregion

    #region プロパティ群
    public Vector2 CurrentMoveDir
    {
        get
        {
            return _currentMoveDir;
        }
        private set
        {
            _currentMoveDir = value;
        }
    }
    #endregion

    #region メンバ変数群
    private Animator _animator = null;
    private Vector2 _currentMoveDir = Vector2.zero;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        var nextMoveDir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            nextMoveDir.y += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            nextMoveDir.y -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            nextMoveDir.x += 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            nextMoveDir.x -= 1;
        }
        nextMoveDir.Normalize();

        _currentMoveDir = Vector2.Lerp(_currentMoveDir, nextMoveDir, 0.5f);
        var addPos = _currentMoveDir * status.Speed * Time.deltaTime;
        transform.position += new Vector3(addPos.x, addPos.y, 0.0f);
        Debug.Log(_currentMoveDir);
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
