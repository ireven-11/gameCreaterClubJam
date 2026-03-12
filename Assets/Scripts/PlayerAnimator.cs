using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    #region メンバ変数群
    private Animator _animator = null;
    private PlayerController _playerController = null;
    private PlayerStatus _status = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    public void Update()
    {
        PlayRunAnim();
        PlayAttackAnim();

        InvertSprite();
    }

    private void PlayRunAnim()
    {
        _animator.SetBool("IsRun", _playerController.IsRun);
        _animator.SetFloat("RunScale", _status.MoveSpeedScale);
    }

    private void PlayAttackAnim()
    {
        if (_playerController.IsAttack)
        {
            _animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// 移動方向に応じてスプライトを反転させる
    /// </summary>
    private void InvertSprite()
    {
        if(_playerController.MoveDir.x == 0) { return; }

        var t = _playerController.MoveDir.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(t * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    #endregion
}
