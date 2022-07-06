using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
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
}
