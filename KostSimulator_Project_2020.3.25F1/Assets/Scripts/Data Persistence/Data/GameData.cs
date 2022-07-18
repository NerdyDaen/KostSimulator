using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    #region additional
    public UnitPoint HungerPoint;
    public UnitPoint FrustrationPoint;
    public UnitPoint StaminaPoint;
    public UnitPoint CompletitionPoint;

    public SemesterSet SemesterPoint;
    public GradeSet CourseScore;
    public int GPA;
    public int DayPassed;

    public ItemCollection item_Book;
    public ItemCollection item_Shampoo;
    public ItemCollection item_Pencil;
    public ItemCollection item_Bag;
    public ItemCollection item_Tie;
    public ItemCollection item_Umbrella;
    public ItemCollection item_Dress;
    public ItemCollection item_Clothes;
    public ItemCollection item_BlueShoes;
    public ItemCollection item_RedShoes;
    public FoodCollection food_Boba;
    public FoodCollection food_FriedRice;
    public FoodCollection food_Indomie;
    public FoodCollection food_IceTea;

    public int GPAStock
    {
        get
        {
            return GPA;
        }
        set
        {
            GPA = value;
            if (GPA > MAX_GPA)
            {
                GPA = MAX_GPA;
            }
            else if (GPA < 0) 
            {
                GPA = 0;
            }
        }
    }

    public void ResetGame()
    {
        HungerPoint.MaximumStock = 100;
        FrustrationPoint.MaximumStock = 0;
        StaminaPoint.MaximumStock = 100;
        CompletitionPoint.MaximumStock = 100;

        SemesterPoint = SemesterSet.Junior;
        CourseScore = GradeSet.Default;
        GPA = 0;
        DayPassed = 0;
    }

    private const int MAX_POINT = 100;
    private const int MAX_GPA = 4;
    #endregion

    #region Pure
    public long lastUpdated; //to store serialized time updated every time we save
    public int frustrationLevel; //to store frustation amount

    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> diamondCollected; //checkpoint
    public AttributesData playerAttributesData;

    //the value defined in this constructor will be the default values
    //the game starts with when there's no data to load
    public GameData() //constructor
    {
        this.frustrationLevel = 0; //initialize value to 0, initial value when starting a new game
        playerPosition = Vector3.zero;
        diamondCollected = new SerializableDictionary<string, bool>();
        playerAttributesData = new AttributesData();
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
    #endregion
}
