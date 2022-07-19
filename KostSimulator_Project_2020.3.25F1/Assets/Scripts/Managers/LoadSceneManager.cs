using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public Transform Player;
    public Transform MainCamera;
    public Transform SubCamera;

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

    public void EnterKos()
    {
        Player.position = new Vector3();
        SubCamera.position = new Vector3();
    }
}
