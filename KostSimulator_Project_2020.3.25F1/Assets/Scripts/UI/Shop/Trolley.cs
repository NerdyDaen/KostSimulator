using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class Trolley : MonoBehaviour
{
    #region Singleton:Trolley

    public static Trolley Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public class Item
    {
        public Sprite Image;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Price;
    }

    public List<Item> ItemsList;

    [SerializeField] GameObject ItemUITemplate; 
    [SerializeField] Transform ItemScrollView;  //content
    
    [SerializeField] Color ActiveItemColor; 
    [SerializeField] Color DefaultItemColor; 

    [SerializeField] Image CurrentItemImage; 
    [SerializeField] TextMeshProUGUI CurrentItemName; 
    [SerializeField] TextMeshProUGUI CurrentItemDescription; 
    [SerializeField] TextMeshProUGUI CurrentItemPrice; 

    GameObject g;
    int newSelectedIndex, previousSelectedIndex;

    void Start()
    {
        GetAvailableItems();
        newSelectedIndex = previousSelectedIndex = 0;
    }

    void GetAvailableItems()
    {
        for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++) //get items from shop
        {
            if (Shop.Instance.ShopItemsList[i].IsPurchased)
            {
                //add all purchased items
                AddItems(Shop.Instance.ShopItemsList[i].Image);
                //AddItemsPrice(Shop.Instance.ShopItemsList[i].Price.ToString());
            }
        }

        SelectItem(newSelectedIndex);
    }

    public void AddItems(Sprite img)
    {
        if (ItemsList == null)
        {
            ItemsList = new List<Item>();
        }
        Item it = new Item() { Image = img };
        ItemsList.Add(it); //add it to itemlist

        //add it to itemscrollview
        g = Instantiate(ItemUITemplate, ItemScrollView);
        g.transform.GetChild(0).GetComponent<Image>().sprite = it.Image;

        //add click event
        g.transform.GetComponent<Button>().AddEventListener(ItemsList.Count - 1, OnItemClick);
    }

    public void AddItemsPrice(TextMeshProUGUI txt)
    {
        if (ItemsList == null)
        {
            ItemsList = new List<Item>();
        }
        Item it = new Item() { Price = txt };
        ItemsList.Add(it); //add it to itemlist

        //add it to itemscrollview
        g = Instantiate(ItemUITemplate, ItemScrollView);
        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = it.Price.ToString();
    }

    void OnItemClick (int ItemIndex)
    {
        SelectItem(ItemIndex);
    }

    void SelectItem(int ItemIndex)
    {
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = ItemIndex;
        ItemScrollView.GetChild(previousSelectedIndex).GetComponent<Image>().color = DefaultItemColor ; //default color
        ItemScrollView.GetChild(newSelectedIndex).GetComponent<Image>().color = ActiveItemColor; //active color

        //changeIconItems
        if (CurrentItemImage != null)
        {
            CurrentItemImage.sprite = ItemsList[newSelectedIndex].Image;
        }
        if (CurrentItemPrice != null)
        {
            CurrentItemPrice.text = ItemsList[newSelectedIndex].Price.ToString();
        }
    }
}
