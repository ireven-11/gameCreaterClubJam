using UnityEngine;

public class AttackTriggerActivator : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float activateTime = 0.0f;
    [SerializeField] private float deactivateTime = 1.0f;
    #endregion

    #region メンバ変数群
    private BoxCollider2D _boxCollider2D = null;
    private Animator _animator = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _boxCollider2D = transform.Find("AttackTrigger").gameObject.GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        ActivateAttackTrigger();
    }

    private void ActivateAttackTrigger()
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("attack")) { return; }

        if(stateInfo.normalizedTime >= deactivateTime)
        {
            _boxCollider2D.enabled = false;
        }
        else if(stateInfo.normalizedTime >= activateTime)
        {
            _boxCollider2D.enabled = true;
        }
    }
    #endregion
}
