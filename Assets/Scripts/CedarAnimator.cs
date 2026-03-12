using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CedarAnimator : MonoBehaviour
{
    #region シリアライズフィールド群
    [Header("当たり判定")]
    [SerializeField] private string attackerTag = "PlayerAttack"; // 攻撃ヒットボックスのタグ
    [Tooltip("被弾判定（Hurtbox）。Death以降は無効化される")]
    [SerializeField] private Collider2D[] hurtboxes2D;
    [Tooltip("地形として残す当たり（Obstacle）。残したい場合は設定")]
    [SerializeField] private Collider2D obstacleCollider2D;
    [Tooltip("Death後も地形コライダーを残すか？")]
    [SerializeField] private bool keepObstacleAfterDeath = true;

    [Header("切り株の見せ方（どちらか）")]
    [Tooltip("Animator内に Log ステートがある（推奨：本設計）")]
    [SerializeField] private bool useAnimatorLogState = true;

    [Tooltip("切り株プレハブに差し替える場合はこちらを設定")]
    [SerializeField] private GameObject stumpPrefab;
    [SerializeField] private Transform stumpSpawnRoot; // 未指定なら this.transform
    [SerializeField] private bool destroyOriginalWhenSpawnStump = true;
    #endregion

    #region メンバ変数群
    private Animator _animator;
    private CedarStatus _status;
    #endregion

    // Animator hashes
    private readonly int HashHit = Animator.StringToHash("Hit");
    private readonly int HashIsDead = Animator.StringToHash("IsDead");

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _status = GetComponent<CedarStatus>();
    }

    public void Update()
    {
        PlayHitAnim();
    }

    private void PlayHitAnim()
    {
        // 前回のHPと変化していたらヒットしたものとする
        if(_status.HP == _status.PrevHP) { return; }
        
        if(_status.IsAlive)
        {
            _animator.SetTrigger("Hit");
        }
        else
        {
            _animator.SetTrigger("Death");
        }
    }

    // === Animation Event: Deathアニメの最終フレームで呼ぶ（推奨） ===
    public void OnDeathAnimationFinished()
    {
        if (useAnimatorLogState)
        {
            // Animatorが Death → Log に遷移するだけ。特別な処理は不要。
            // （必要ならここでエフェクトやドロップ生成など）
            return;
        }

        // プレハブ差し替え方式
        SpawnStump();
    }

    // 切り株プレハブを生成して差し替える（差し替え方式を選んだ場合に使用）
    public void SpawnStump()
    {
        if (stumpPrefab == null)
        {
            Debug.LogWarning("[TreeDamageable] stumpPrefab が未設定です。Animator Log ステートを使うか、Prefabを割り当ててください。");
            return;
        }

        var root = stumpSpawnRoot != null ? stumpSpawnRoot : transform;
        var stump = Instantiate(stumpPrefab, root.position, Quaternion.identity);
        stump.transform.localScale = transform.localScale; // 左右反転などを継承

        if (destroyOriginalWhenSpawnStump)
        {
            Destroy(gameObject);
        }
        else
        {
            // 元を残す場合は見た目/当たりなどを無効化
            if (obstacleCollider2D != null && !keepObstacleAfterDeath) obstacleCollider2D.enabled = false;
            var sr = GetComponentInChildren<SpriteRenderer>();
            if (sr) sr.enabled = false;
            var rb2d = GetComponent<Rigidbody2D>();
            if (rb2d) rb2d.simulated = false;
            enabled = false;
        }
    }

}
