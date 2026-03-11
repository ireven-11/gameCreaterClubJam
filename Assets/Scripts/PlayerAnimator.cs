using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    #region メンバ変数群
    private PlayerController _playerController = null;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Update()
    {
        InvertAnimation();
    }

    private void InvertAnimation()
    {
        if (!_playerController.IsMove) { return; }

        var t = _playerController.MoveDir.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(t * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
