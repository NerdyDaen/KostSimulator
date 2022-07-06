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
}
