using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("Main Settings")]
    public float DataTimer;
    public Text TextTimer;

    [Header("Condition")]
    public UnityEvent TimerFinishEvent;
    bool isExecuted = false;

    void Start()
    {
        InvokeRepeating("StartTimer", 1, 1);
    }

    void StartTimer()
    {
        if (DataTimer > 0)
        {
            DataTimer--;
            if (TextTimer != null)
            {
                TextTimer.text = DataTimer.ToString();
            }
        }

        if (DataTimer == 0)
        {
            TimerFinishEvent.Invoke();
            DataTimer = -1;
        }
    }
}
