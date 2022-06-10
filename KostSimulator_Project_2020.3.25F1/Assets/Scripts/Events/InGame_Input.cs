using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGame_Input : MonoBehaviour
{
    [Header("Main Settings")]
    public KeyCode TargetKey;
    public UnityEvent InputEvent;

    void Update()
    {
        if (Input.GetKeyDown(TargetKey))
        {
            InputEvent.Invoke();
        }
    }
}
