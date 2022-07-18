using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton:GameManager
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] TextMeshProUGUI[] allMoneyUIText;

    public int Money;

    void Start()
    {
        UpdateAllMoneyUIText();
    }

    #region Money
    public void UseMoney(int amount)
    {
        Money -= amount;
    }

    public bool HasEnoughMoney(int amount)
    {
        return (Money >= amount);
    }

    public void UpdateAllMoneyUIText()
    {
        for (int i = 0; i < allMoneyUIText.Length; i++)
        {
            allMoneyUIText[i].text = Money.ToString();
        }
    }
    #endregion
    #region Gameplay Manager
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    #endregion
    #region Data Manager

    public SemesterSet SemesterPoint;
    public GradeSet CourseScore;
    public int GPA;
    public int DayPassed;

    #region Items and Foods
    [SerializeField] private int itemBook;
    [SerializeField] private int itemDress;
    [SerializeField] private int itemShampoo;
    [SerializeField] private int itemPencil;
    [SerializeField] private int itemBag;
    [SerializeField] private int itemTie;
    [SerializeField] private int itemClothes;
    [SerializeField] private int itemBlueShoes;
    [SerializeField] private int itemRedShoes;
    [SerializeField] private int itemUmbrella;
    [SerializeField] private int foodBoba;
    [SerializeField] private int foodFriedRice;
    [SerializeField] private int foodIndomie;
    [SerializeField] private int foodIceTea;

    public int Food_Boba
    {
        get
        {
            return foodBoba;
        }
        set
        {
            foodBoba = value;
            if (foodBoba > 99)
            {
                foodBoba = 99;
            }
        }
    }

    public int Food_FriedRice
    {
        get
        {
            return foodFriedRice;
        }
        set
        {
            foodFriedRice = value;
            if (foodFriedRice > 99)
            {
                foodFriedRice = 99;
            }
        }
    }

    public int Food_Indomie
    {
        get
        {
            return foodIndomie;
        }
        set
        {
            foodIndomie = value;
            if (foodIndomie > 99)
            {
                foodIndomie = 99;
            }
        }
    }

    public int Food_IceTea
    {
        get
        {
            return foodIceTea;
        }
        set
        {
            foodIceTea = value;
            if (foodIceTea > 99)
            {
                foodIceTea = 99;
            }
        }
    }
    #endregion
    #endregion
}

[Serializable]
public class ItemCollection
{
    public const int MAX_ITEM = 999;

    public ItemSet Uid;
    public string Name;
    public string Description;
    public int Price;
    public int Amount;
    public bool IsStack; //material/ consumable/ equipment? -> equipment cant be stack

    public int Stock
    {
        get
        {
            return Amount;
        }
        set
        {
            Amount = value;
            if (Amount > MAX_ITEM)
            {
                Amount = MAX_ITEM;
            }
            else if (Amount < 0)
            {
                Amount = 0;
            }
        }
    }

    public static ItemCollection operator +(ItemCollection a, int b)
    {
        a.Stock = a.Amount + b;
        return a;
    }

    public static ItemCollection operator -(ItemCollection a, int b)
    {
        a.Stock = a.Amount - b;
        return a;
    }
}

[Serializable] 
public class FoodCollection
{
    public const int MAX_FOOD = 99;

    public FoodSet Uid;
    public string Name;
    public string Description;
    public int Price;
    public int Amount;
    public bool IsStack; //material/ consumable/ equipment? -> equipment cant be stack

    public int Stock
    {
        get
        {
            return Amount;
        }
        set
        {
            Amount = value;
            if (Amount > MAX_FOOD)
            {
                Amount = MAX_FOOD;
            }
            else if (Amount < 0)
            {
                Amount = 0;
            }
        }
    }

    public static FoodCollection operator +(FoodCollection a, int b)
    {
        a.Stock = a.Amount + b;
        return a;
    }

    public static FoodCollection operator -(FoodCollection a, int b)
    {
        a.Stock = a.Amount - b;
        return a;
    }
}

public enum UnitState
{
    Idle, Bored, Walk, Run, 
}

public enum SemesterSet
{
    Default,
    Junior, Mid, Senior,
}

public enum ItemSet
{
    Default,
    Book, Shampoo, Pencil, Bag, Tie, Umbrella,
    Dress, Clothes, BlueShoes, RedShoes,
}

public enum FoodSet
{
    Default,
    Boba, FriedRice, Indomie, IceTea,
}

public enum GradeSet
{
    Default,
    E, D, C, B, A,
}
