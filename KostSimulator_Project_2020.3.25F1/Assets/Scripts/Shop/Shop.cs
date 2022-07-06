using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    #region Singleton:Shop

    public static Shop Instance;

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

    [System.Serializable] public class ShopItem
    {
        public Sprite Image;
        public int Price;
        public string Name;
        public string Description;
        public bool IsPurchased = false;
        
    }

    public List<ShopItem> ShopItemsList;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] Transform ItemDescPanel;
    [SerializeField] public Animator NoMoneyAnim;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] GameObject ItemTemplate;
    [SerializeField] GameObject DescTemplate;
    GameObject g;
    GameObject h;
    Button buyBtn;
    Image soldOutImage;

    private void Start()
    {
        //ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        //DescTemplate = ItemDescPanel.GetChild(0).gameObject;

        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Name;
            g.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(3).GetComponent<Button>();

            if (ShopItemsList[i].IsPurchased)
            {
                DisableBuyButton();
            }
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);

            h = Instantiate(DescTemplate, ItemDescPanel);
            h.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Name;
            h.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Description;
        }

        //Destroy(ItemTemplate);
        //Destroy(DescTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if (GameManager.instance.HasEnoughMoney(ShopItemsList[itemIndex].Price))
        {
            GameManager.instance.UseMoney(ShopItemsList[itemIndex].Price);
            //puchase item
            ShopItemsList[itemIndex].IsPurchased = true;

            //disable the button
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
            DisableBuyButton();
            //menampilkan image sold out
            soldOutImage = ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Image>();
            soldOutImage.enabled = true;

            //change UI text: money
            GameManager.instance.UpdateAllMoneyUIText();

            //add items
            Trolley.Instance.AddItems(ShopItemsList[itemIndex].Image);
        }
        else
        {
            NoMoneyAnim.SetTrigger("OnNoMoney");
            Debug.Log("You don't have enough money!!");
        }
    }

    void DisableBuyButton()
    {
        buyBtn.interactable = false;
    }


    /*---------------------------------- Open & Close Shop ----------------------------------*/
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }
}
