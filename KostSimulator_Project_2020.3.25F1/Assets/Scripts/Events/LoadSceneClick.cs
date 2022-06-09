using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneClick : MonoBehaviour
{
    [Header("Main Settings")]
    public string TargetScene;

    public void LoadScene()
    {
        StartCoroutine(clickDelay());
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
    }

    IEnumerator clickDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(TargetScene);
    }
}
