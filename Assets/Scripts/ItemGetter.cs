using UnityEngine;
using UnityEngine.InputSystem;

public class ItemGetter : MonoBehaviour
{
    #region メンバ変数群
    private PlayerStatus _status = null;
    private GameObject _getItemObject = null;
    private ItemHolder _itemHolder = null;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _status = GetComponent<PlayerStatus>();
        _itemHolder = GetComponent<ItemHolder>();
    }

    // Update is called once per frame
    public void Update()
    {
        GetItem(_getItemObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
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
        var item = gameObject.GetComponent<Item>();
        if (item == null) { return; }

        if (!item.CanAcquired) { return; }

        // コストを確認
        if (!_itemHolder.ConsumeWood(item.Cost)) { return; }

        item.OnAcquired(_status);
        _itemHolder.AddItem(item);
    }
    #endregion
}
