using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        // create a new game - which will initialize our game data
        DataPersistenceManager.instance.NewGame();
        //load the gameplay scene - which will in turn save the game because of
        //OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("GamePlay");
    }

    public void OnLoadGameClicked()
    {
        DisableMenuButtons();
        //load the next scene - which will return load the game because of
        //OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("GamePlay");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
    }
}
