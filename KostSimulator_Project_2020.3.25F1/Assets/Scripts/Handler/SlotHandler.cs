using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    public ItemSet Item;
    public FoodSet Food;
    public GameData Data;

    public Action<ItemCollection> SlotItemAction { get; set; }
    public Action<FoodCollection> SlotFoodAction { get; set; }

    public ItemCollection ItemSelected
    {
        get
        {
            switch (Item)   
            {
                case ItemSet.Book: return Data.item_Book;
                case ItemSet.Shampoo: return Data.item_Shampoo;
                case ItemSet.Pencil:return Data.item_Pencil;
                case ItemSet.Bag: return Data.item_Bag;
                case ItemSet.Tie: return Data.item_Tie;
                case ItemSet.Umbrella: return Data.item_Umbrella;
                case ItemSet.Dress: return Data.item_Dress;
                case ItemSet.Clothes: return Data.item_Clothes;
                case ItemSet.BlueShoes: return Data.item_BlueShoes; 
                case ItemSet.RedShoes: return Data.item_RedShoes;
            }
            return null;
        }
    }

    public FoodCollection FoodSelected
    {
        get
        {
            switch (Food)   
            {
                case FoodSet.Boba: return Data.food_Boba;
                case FoodSet.FriedRice: return Data.food_FriedRice;
                case FoodSet.Indomie: return Data.food_Indomie;
                case FoodSet.IceTea: return Data.food_IceTea;
            }
            return null;
        }
    }

    public void BtnSlot_Handler() //determine which item that player select
    {
        SlotItemAction?.Invoke(ItemSelected);
        SlotFoodAction?.Invoke(FoodSelected);
    }
}
