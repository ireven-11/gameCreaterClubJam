using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animation_Lv2 : MonoBehaviour
{

    [Header("ステータス")]
    public int maxHP = 10;
    public float hitInvincibleTime = 0.1f; // 連続ヒット保護（必要なければ0でもOK）

    [Header("当たり判定")]
    public string attackerTag = "PlayerAttack"; // 攻撃ヒットボックスのタグ
    [Tooltip("被弾判定（Hurtbox）。Death以降は無効化される")]
    public Collider2D[] hurtboxes2D;
    [Tooltip("地形として残す当たり（Obstacle）。残したい場合は設定")]
    public Collider2D obstacleCollider2D;
    [Tooltip("Death後も地形コライダーを残すか？")]
    public bool keepObstacleAfterDeath = true;

    [Header("切り株の見せ方（どちらか）")]
    [Tooltip("Animator内に Log ステートがある（推奨：本設計）")]
    public bool useAnimatorLogState = true;

    [Tooltip("切り株プレハブに差し替える場合はこちらを設定")]
    public GameObject stumpPrefab;
    public Transform stumpSpawnRoot; // 未指定なら this.transform
    public bool destroyOriginalWhenSpawnStump = true;

    private Animator anim;
    private int currentHP;
    private float lastHitTime = -999f;
    private bool isDead = false;

    // Animator hashes
    private readonly int HashHit = Animator.StringToHash("Hit");
    private readonly int HashIsDead = Animator.StringToHash("IsDead");

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    // ==== 2D（Trigger推奨）====
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(attackerTag))
        {
            int damage = 1;
            var payload = other.GetComponent<HitPayload>();
            if (payload != null) damage = payload.damage;

            ApplyDamage(damage);
        }
    }



    public void ApplyDamage(int damage)
    {
        if (isDead) return;

        // 連続ヒット保護
        if (Time.time < lastHitTime + hitInvincibleTime) return;
        lastHitTime = Time.time;

        currentHP = Mathf.Max(0, currentHP - damage);

        if (currentHP <= 0)
        {
            isDead = true;

            // まず被弾判定を止める（以降は攻撃が当たらない）
            SetHurtboxesActive(false);

            // 地形コライダーの扱い
            if (!keepObstacleAfterDeath && obstacleCollider2D != null)
                obstacleCollider2D.enabled = false;

            // Animator経由でDeath→Log（推奨）
            anim.SetBool(HashIsDead, true);

            // もし「切り株プレハブ差し替え」を使うなら、Deathアニメ終端のAnimationEventから SpawnStump() を呼ぶ
            return;
        }

        // 生存時はHitアニメ
        anim.ResetTrigger(HashHit);
        anim.SetTrigger(HashHit);
    }

    private void SetHurtboxesActive(bool active)
    {
        if (hurtboxes2D != null)
        {
            foreach (var c in hurtboxes2D)
                if (c != null) c.enabled = active;
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