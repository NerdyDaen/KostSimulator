using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigations")]
    [SerializeField] private SaveSlotsMenu saveSlotMenu;


    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start()
    {
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        saveSlotMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        saveSlotMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        //save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();
        //load the next scene - which will return load the game because of
        //OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("GamePlay");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
