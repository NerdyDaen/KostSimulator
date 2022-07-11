using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesData
{
    //status data
    public int hunger;
    public int stamina;
    public int intellectScore;
    public int frustration;

    public AttributesData()
    {
        this.hunger = 100;
        this.stamina = 100;
        this.intellectScore = 0;
        this.frustration = 0;
    }
}
