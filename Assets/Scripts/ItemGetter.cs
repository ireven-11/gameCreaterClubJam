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
    private List<GameObject> _woods = new List<GameObject>();
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
        OnChaseWoodItem();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Wood")
        {
            _woods.Add(collision.gameObject);
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

    private void OnChaseWoodItem()
    {
        for (int i = _woods.Count - 1; i >= 0; i--)
        {
            var wood = _woods[i];

            var dir = (transform.position - wood.transform.position).normalized;
            wood.transform.position += dir * woodChaseSpeed * Time.deltaTime;

            var distance = (transform.position - wood.transform.position).magnitude;

            if (distance < woodGetDistance)
            {
                _itemHolder.AddWood(1);
                _woods.RemoveAt(i);
            }
        }
    }
    #endregion
}
