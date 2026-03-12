
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance;

    public int currentExp = 0;
    public int nextExp = 10;
    public int level = 1;

    public LevelGauge uiGauge; // UIゲージへの参照

    void Awake()
    {
        Instance = this;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;

        // レベルアップ処理
        if (currentExp >= nextExp)
        {
            currentExp -= nextExp;
            level++;
            nextExp += 10; // 必要EXP増やすなど
        }

        // ゲージ更新
        uiGauge.UpdateGauge(currentExp, nextExp);
    }
}
