using UnityEngine;

public class CedarStatus : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private int level  = 0;
    [SerializeField] private float hp   = 0.0f;
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
        get { return isAlive; }
        private set { isAlive = value; }
    }
    #endregion

    #region メンバ変数群
    private bool isAlive = true;
    private float maxHp = 0.0f;
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive) { return; }
        if (collision.tag != "Axe") { return; }

        GameObject player = collision.transform.parent.gameObject;

        PlayerStatus status = player.GetComponent<PlayerStatus>();

        ItemHolder holder = player.GetComponent<ItemHolder>();
        Item item = holder.GetItem("PowerUpItem");

        float damage = status.Power;
        float damegeRate = LevelCheck(item != null ? item.Level : 0);
        damage *= damegeRate;

        OnDamage(damage);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private float LevelCheck(int power_level)
    {
        if (level < power_level)
        {
            return 0.5f;
        }
        return 1.0f;
    }
    private void OnDamage(float damage)
    {
        hp -= damage;
        if (hp < 0.0f)
        {
            isAlive = false;
        }
    }
}
