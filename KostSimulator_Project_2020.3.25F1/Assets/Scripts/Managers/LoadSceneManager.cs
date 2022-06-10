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
        StartCoroutine(clickDelay());
    }
    IEnumerator clickDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(TargetScene);
    }
}
