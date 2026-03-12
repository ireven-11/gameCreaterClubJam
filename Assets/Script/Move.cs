using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class Move : MonoBehaviour
{


    [Header("移動（ピクセル/秒）")]
    [Tooltip("移動方向。右上→右下に斜めなら (0.2, -1) など（X>0, Y<0）。")]
    public Vector2 direction = new Vector2(0f, -1f);
    public float speed = 200f;

    [Header("初期位置（右上基準の anchoredPosition を直接入力）")]
    [Tooltip("チェックONで、現在の anchoredPosition を初期位置として採用します。")]
    public bool useCurrentAnchoredPositionAsStart = false;

    [Tooltip("右上アンカー基準の初期座標。X: 右が正 → 右へ +、Y: 上が正 → 下へ -")]
    public Vector2 startAnchoredPos = new Vector2(-20f, -20f);

    [Header("画面外で戻す（どれだけ外へ出たらリセットするか）")]
    public float resetMargin = 50f;

    private RectTransform rt;
    private RectTransform canvasRt;
    private Vector2 startPosRuntime; // 実際に使う初期位置（起動時に決定）
    private Vector2 dirNorm;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        canvasRt = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // 基準のズレ防止：UI座標を右上基準に固定
        rt.anchorMin = rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(1f, 1f);
    }

    void Start()
    {
        // 方向は正規化して一定速度に
        dirNorm = (direction.sqrMagnitude > 0f) ? direction.normalized : Vector2.down;

        // 実行時の初期位置を確定（現在値を使う or Inspectorの値を使う）
        startPosRuntime = useCurrentAnchoredPositionAsStart ? rt.anchoredPosition : startAnchoredPos;
        rt.anchoredPosition = startPosRuntime;
    }

    void Update()
    {
        // 進行
        rt.anchoredPosition += dirNorm * speed * Time.deltaTime;

        // Canvas の見かけサイズを取得
        float w = canvasRt.rect.width;
        float h = canvasRt.rect.height;

        // 右上アンカー基準の可視範囲は X: [-w, 0], Y: [-h, 0]
        Vector2 p = rt.anchoredPosition;

        bool outRight = p.x > 0f + resetMargin;
        bool outLeft = p.x < -w - resetMargin;
        bool outTop = p.y > 0f + resetMargin;
        bool outBottom = p.y < -h - resetMargin;

        if (outRight || outLeft || outTop || outBottom)
        {
            // 右上に戻す（Inspector指定 or 現在値採用）
            rt.anchoredPosition = startPosRuntime;
        }
    }

    // Inspector操作時のヒューマンエラー低減
    void OnValidate()
    {
        // direction の正規化（0ベクトルは放置）
        if (direction.sqrMagnitude > 0f)
            direction = direction.normalized;

        // 右上基準の直感に合わせるメモ：
        // X: 右が正（右に行くほど +）、左に行くほど -
        // Y: 上が正（上に行くほど +）、下に行くほど -
    }



}
