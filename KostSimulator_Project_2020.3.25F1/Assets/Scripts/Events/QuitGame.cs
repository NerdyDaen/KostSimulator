using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        //mematikan game saat dalam editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        //mematikan game setelah di build
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
