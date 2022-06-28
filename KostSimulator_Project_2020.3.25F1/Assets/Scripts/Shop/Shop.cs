using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public string Name;
        public string Description;
        public bool IsPurchased = false;
        
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    public bool IsSelected = false;
    GameObject ItemTemplate;
    GameObject DescTemplate;
    GameObject g;
    GameObject h;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] Transform ItemDescPanel;

    private void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        DescTemplate = ItemDescPanel.GetChild(0).gameObject;

        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Name;
            g.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Price.ToString();
            g.transform.GetChild(3).GetComponent<Button>().interactable = !ShopItemsList[i].IsPurchased;

            //if (IsSelected == true)
            //{
                h = Instantiate(DescTemplate, ItemDescPanel);
                h.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Name;
                h.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Description;
            //}

            //if (IsSelected == false)
            //{
                
            //}
        }

        Destroy(ItemTemplate);
        Destroy(DescTemplate);
    }

    public void OnSelected()
    {
        IsSelected = true;
    }

    public void OnDeselected()
    {
        IsSelected = false;
    }
}
