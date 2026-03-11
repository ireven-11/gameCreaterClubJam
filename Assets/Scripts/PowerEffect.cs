using UnityEngine;

public class PowerEffect : ItemLevel
{
    public float powerUp;

    public override float Apply()
    {
       return powerUp;
    }
}
