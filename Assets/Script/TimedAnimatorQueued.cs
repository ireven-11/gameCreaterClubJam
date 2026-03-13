using UnityEngine;

public class TimedAnimatorQueued : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // 切り替え対象のState名（Base Layer直下を想定）
    [SerializeField] private string[] stateNames = { "Idle", "Dash", "Fall" };

    [Tooltip("切替を要求する間隔（秒）。この時点では即切替せず、現行クリップの終了を待ちます。")]
    [SerializeField] private float interval = 5f;

    [Tooltip("切り替え順序：false=順番、true=ランダム")]
    [SerializeField] private bool randomOrder = false;

    [Tooltip("クロスフェード（秒）。0で即時切替")]
    [SerializeField, Range(0f, 0.5f)] private float crossFadeDuration = 0.15f;

    private float timer;
    private int index;
    private bool switchQueued;
    private int layer = 0;

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 1) インターバル経過で「切替予約」を立てる（即切替しない）
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            switchQueued = true; // 予約フラグON
        }

        // 2) 予約があり、かつ現在クリップの再生が完了したら切替実行
        if (switchQueued && IsCurrentClipFinished(animator, layer))
        {
            switchQueued = false;
            string nextState = GetNextStateName();
            if (crossFadeDuration > 0f)
                animator.CrossFade(nextState, crossFadeDuration, layer, 0f);
            else
                animator.Play(nextState, layer, 0f);
        }
    }

    // 現在のステートが「終わっている」か判定（非遷移中 & normalizedTime >= 1）
    private bool IsCurrentClipFinished(Animator anim, int layerIndex)
    {
        if (anim.IsInTransition(layerIndex)) return false;

        var info = anim.GetCurrentAnimatorStateInfo(layerIndex);
        // ループしていると normalizedTime は増え続ける（%1 でループ）ので、
        // 「ループOFFのクリップ」に対してのみ確実に効く。ループONの場合は ExitTime を使う方法（下の方法2）が適。
        return info.normalizedTime >= 1f && !info.loop;
    }

    private string GetNextStateName()
    {
        if (stateNames == null || stateNames.Length == 0) return "";

        if (randomOrder)
        {
            // 同じステート連続を避けたい場合はループで選び直すなど工夫
            int next = Random.Range(0, stateNames.Length);
            return stateNames[next];
        }
        else
        {
            index = (index + 1) % stateNames.Length;
            return stateNames[index];
        }
    }
}
