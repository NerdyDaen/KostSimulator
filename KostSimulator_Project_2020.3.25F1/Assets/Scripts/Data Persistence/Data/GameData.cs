using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated; //to store serialized time updated every time we save
    public int frustrationLevel; //to store frustation amount

    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> diamondCollected; //checkpoint

    //the value defined in this constructor will be the default values
    //the game starts with when there's no data to load
    public GameData() //constructor
    {
        this.frustrationLevel = 0; //initialize value to 0, initial value when starting a new game
        playerPosition = Vector3.zero;
        diamondCollected = new SerializableDictionary<string, bool>();
    }

    public int GetPercentageComplete() //NOT FIX YET - WILL BE CHANGED TO HOW MANY DAYS PLAYER PASSED
    {
        //figure out how many diamonds we've colleted
        int totalCollected = 0;
        foreach(bool collected in diamondCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        //ensure we don't devide by 0 when calculating the percentage
        int percentageComplete = -1;
        if (diamondCollected.Count != 0)
        {
            percentageComplete = (totalCollected * 100 / diamondCollected.Count);
        }
        return percentageComplete;
    }

    public int GetDiamondCollected()
    {
        //figure out how many diamonds we've colleted
        int totalDiamondCollected = 0;
        foreach (bool collected in diamondCollected.Values)
        {
            if (collected)
            {
                totalDiamondCollected++;
            }
        }
        return totalDiamondCollected;
    }
}
