
using UnityEngine;
using UnityEngine.UI;

public class LevelGauge : MonoBehaviour
{
    public Image fillImage;

    public void UpdateGauge(int currentExp, int nextExp)
    {
        fillImage.fillAmount = (float)currentExp / nextExp;
    }
}
