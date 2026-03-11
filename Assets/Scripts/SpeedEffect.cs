using UnityEngine;

public class SpeedEffect : ItemLevel
{
    public float speedUp;

    public override float Apply()
    {
        return speedUp;
    }
}
