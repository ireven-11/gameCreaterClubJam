using UnityEngine;

public class CedarStatus : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private int level = 0;
    [SerializeField] private float hp = 0.0f;
    [SerializeField] private GameObject dropPrefab = null;
    #endregion

    #region プロパティ群
    public int Level
    {
        get { return level; }
        private set { level = value; }
    }

    public float HP
    {
        get { return hp; }
        private set { hp = value; }
    }

    public bool IsAlive
    {
        get { return _isAlive; }
        private set { _isAlive = value; }
    }
    #endregion

    #region メンバ変数群
    private bool _isAlive = true;
    public bool _isDrop = false;
    #endregion

    #region 関数群
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_isDrop)
        {
            SpawnDrop();
            _isDrop = false;
        }        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isAlive) { return; }
        if (collision.gameObject.name != "AttackTrigger") { return; }

        var parent = collision.transform.parent.gameObject;
        if(parent.name != "Player") { return; }

        var status = parent.GetComponent<PlayerStatus>();
        var holder = parent.GetComponent<ItemHolder>();
        var item = holder.GetItem("PowerUpItem");

        // ダメージ計算
        var damage = status.Power;
        var damegeRate = LevelCheck(item != null ? item.Level : 1);
        damage *= damegeRate;

        OnDamage(damage);
    }

    public void SpawnDrop()
    {
        var prefab = Instantiate(dropPrefab);
        var item = prefab.GetComponent<DropItem>();
        item.DropFromTree(transform.position);
    }


    private float LevelCheck(int power_level)
    {
        return level < power_level ? 0.5f : 1.0f;
    }

    private void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0.0f)
        {
            hp = 0.0f;
            _isAlive = false;
            SpawnDrop();
        }
    }
    #endregion
}
