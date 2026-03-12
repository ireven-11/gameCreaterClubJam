using UnityEngine;

public class DropItem : MonoBehaviour
{
    #region シリアライズフィールド群
    [SerializeField] private float jumpHeight = 1f;     // 跳ねる高さ
    [SerializeField] private float speed = 3f;           // 落下速度
    [SerializeField] private float horizontalRange = 0.5f; // 木の下で少し横にずらす
    [SerializeField] private float dropDistance = 2f;   // 木から落ちる距離
    #endregion

    #region メンバ変数群
    private Vector3 startPos;
    private Vector3 targetPos;
    private float height;      // 上下の弧の高さ
    private bool isDropping = false;
    private float t;           // 補間パラメータ 0→1
    #endregion

    // 木からドロップするときに呼ぶ
    public void DropFromTree(Vector3 treePosition)
    {
        float xOffset = Random.Range(-horizontalRange, horizontalRange);

        startPos = treePosition;
        targetPos = new Vector3(treePosition.x + xOffset, treePosition.y - dropDistance, treePosition.z);

        height = jumpHeight;
        t = 0f;
        transform.position = startPos;
        isDropping = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropping)
        {
            t += speed * Time.deltaTime;

            // 線形補間でX/Yの位置
            Vector3 pos = Vector3.Lerp(startPos, targetPos, t);

            // 上下の弧（ジャンプの高さ）を追加
            float arc = Mathf.Sin(t * Mathf.PI) * height;
            pos.y += arc;

            transform.position = pos;

            if (t >= 1f)
            {
                transform.position = targetPos;
                isDropping = false;
            }
        }
    }
}
