using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    public float speed = 0.0f;
    public float attack = 0.0f;

    public Status(float speed, float attack)
    {
        this.speed = speed;
        this.attack = attack;
    }

    public Status()
    {
        this.speed = 0.0f;
        this.attack = 0.0f;
    }
}