using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [Header("Main Settings")]
    public string TagObject;
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    public bool DestroyTrigger;

    public void InvokeTrigger()
    {
        TriggerEnter.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagObject)
        {
            TriggerEnter.Invoke();
            if (DestroyTrigger)
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagObject)
        {
            TriggerExit.Invoke();
            if (DestroyTrigger)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
