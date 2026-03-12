using UnityEngine;

public class DropItem : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float speed = 0;
    #endregion

    #region メンバ変数群
    public bool drop = false;
    private Vector2 direction;
    private Vector3 dropPosition;
    private Rigidbody2D rb;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float angle = Random.Range(30f, 150f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        dropPosition = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(drop)
        {
            rb.gravityScale = 1f;
            this.transform.position += (Vector3)direction * speed * Time.deltaTime;
            float distance = Vector3.Distance(this.transform.position, dropPosition);
            if(distance > 4) { drop = false; }
        }
        else
        {
            rb.gravityScale = 0f;
        }
    }
}
