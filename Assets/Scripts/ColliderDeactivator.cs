using UnityEngine;

public class ColliderDeactivator : MonoBehaviour
{
    #region ƒƒ“ƒo•Ï”ŒQ
    private BoxCollider2D boxCollider2D = null;
    private CedarStatus _status = null;
    #endregion

    #region ŠÖ”ŒQ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        _status = GetComponent<CedarStatus>();
    }

    // Update is called once per frame
    public void Update()
    {
        Deactivate();
    }

    private void Deactivate()
    {
        if (_status.IsAlive) { return; }

        boxCollider2D.enabled = false;
    }
    #endregion
}
