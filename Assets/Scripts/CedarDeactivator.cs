using UnityEngine;

public class CedarDeactivator : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float transparentSpeed = 1.0f;
    [SerializeField] private float transparentWaitTime = 1.0f;
    #endregion

    #region メンバ変数群
    private BoxCollider2D boxCollider2D = null;
    private CedarStatus _status = null;
    private SpriteRenderer _spriteRenderer = null;
    private float _deathTime = 0.0f;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        _status = GetComponent<CedarStatus>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(!_status.IsAlive)
        {
            _deathTime += Time.deltaTime;
        }

        DeactivateCollider();
        MakeTransparent();
    }

    private void DeactivateCollider()
    {
        if (_status.IsAlive) { return; }

        boxCollider2D.enabled = false;
    }

    private void MakeTransparent()
    {
        if(_deathTime < transparentWaitTime) { return; }

        _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a - transparentSpeed * Time.deltaTime);
    }
    #endregion
}
