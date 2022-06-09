using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [Header("Main Settings")]
    public string TargetScene;

    public void LoadScene()
    {
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
        SceneManager.LoadScene(TargetScene);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
