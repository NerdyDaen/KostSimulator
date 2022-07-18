using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIActive : MonoBehaviour
{
    public Transform Player;
    public Transform MainCamera;
    public Transform SubCamera;
    #region Public UI
    public void BtnProfile_Handler()
    {
        if (PnlProfile.gameObject.activeInHierarchy) //agar satu tombol bisa buka n tutup panel
        {
            PnlProfile.gameObject.SetActive(true);
        }
        else
        {
            DisableMenuPanel();
            PnlProfile.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Private UI
    [Header("Panel Menu List")]
    [SerializeField] private Transform PnlProfile;
    [SerializeField] private Transform PnlStatus;
    [SerializeField] private Transform PnlInventory;
    [SerializeField] private Transform PnlQuest;
    [SerializeField] private Transform PnlTask;
    [SerializeField] private Transform PnlDialogue;
    [SerializeField] private Transform PnlMarket;
    [SerializeField] private Transform PnlCafe;
    [SerializeField] private Transform PnlUniversity;
    [SerializeField] private Transform PnlLaptop;

    [Header("Panel Sub-Menu List")]
    [SerializeField] private Transform PnlTutorial;
    [SerializeField] private Transform PnlStory;
    [SerializeField] private Transform PnlMarketBuy;
    [SerializeField] private Transform PnlMarketSell;
    [SerializeField] private Transform PnlCafeBuy;
    [SerializeField] private Transform PnlCafeSell;

    [Header("Text List")]
    [SerializeField] private TextMeshProUGUI[] TxtCoin;
    [SerializeField] private TextMeshProUGUI[] TxtPlayerName;
    [SerializeField] private TextMeshProUGUI[] TxtPlayerGPA;
    [SerializeField] private TextMeshProUGUI[] TxtDayPassed;
    [SerializeField] private TextMeshProUGUI TxtDayNow;
    [SerializeField] private TextMeshProUGUI[] TxtFrustrationLevel;
    [SerializeField] private TextMeshProUGUI[] TxtHungerLevel;
    [SerializeField] private TextMeshProUGUI[] TxtItemQuantity;
    [SerializeField] private TextMeshProUGUI[] TxtItemName;
    [SerializeField] private TextMeshProUGUI[] TxtItemDesc;

    [Header("Slider List")]
    [SerializeField] private Slider SldFrustrationLevel;
    [SerializeField] private Slider SldDayPassed;
    [SerializeField] private Slider[] SldHungerLevel;
    #endregion

    private void DisableMenuPanel()//Close All Panel
    {
        PnlProfile.gameObject.SetActive(false);
        PnlStatus.gameObject.SetActive(false);
        PnlInventory.gameObject.SetActive(false);
        PnlQuest.gameObject.SetActive(false);
        PnlTask.gameObject.SetActive(false);
    }

    private void DisableSubMenuPanel()//Close All Panel
    {
        PnlTutorial.gameObject.SetActive(false);
        PnlStory.gameObject.SetActive(false);
        PnlMarketBuy.gameObject.SetActive(false);
        PnlMarketSell.gameObject.SetActive(false);
        PnlCafeBuy.gameObject.SetActive(false);
        PnlCafeSell.gameObject.SetActive(false);
    }
}
