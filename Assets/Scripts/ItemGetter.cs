using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemGetter : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float woodGetDistance = 1.0f;  // 木材を取得する距離
    [SerializeField] private float woodChaseSpeed = 20.0f;
    #endregion

    #region メンバ変数群
    private PlayerStatus _status = null;
    private GameObject _getItemObject = null;
    private ItemHolder _itemHolder = null;
    private List<TargetChaser> _woods = new List<TargetChaser>();
    private Transform _ownerTransform = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _status = GetComponent<PlayerStatus>();
        _itemHolder = GetComponent<ItemHolder>();
        _ownerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        GetItem(_getItemObject);
        GetWood();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wood")
        {
            var targetChaser = collision.gameObject.GetComponent<TargetChaser>();
            if(targetChaser == null) { return; }

            _woods.Add(targetChaser);
            targetChaser.StartChase(_ownerTransform);
        }
        else if(collision.gameObject.tag == "Item")
        {
            _getItemObject = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            _getItemObject = null;
        }
    }

    private void GetItem(GameObject gameObject)
    {
        // 取得可能かを確認
        if (_getItemObject == null) { return; }
        if (!Keyboard.current.fKey.wasPressedThisFrame) { return; }
        var item = gameObject.GetComponent<ItemBase>();
        if (item == null) { return; }

        if (!item.CanAcquired) { return; }

        // コストを確認
        if (!_itemHolder.ConsumeWood(item.Cost)) { return; }

        item.OnAcquired(_status);
        _itemHolder.AddItem(item);
    }

    private void GetWood()
    {
        // ループ中に削除するため逆順でループ
        for (int i = _woods.Count - 1; i >= 0; i--)
        {
            if (_woods[i].HasReachedTarget)
            {
                _itemHolder.AddWood(1);
                Destroy(_woods[i].gameObject);
                _woods.Remove(_woods[i]);
            }
        }
    }
    #endregion
}
