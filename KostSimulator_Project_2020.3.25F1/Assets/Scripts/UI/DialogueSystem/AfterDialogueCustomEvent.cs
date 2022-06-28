using UnityEngine;
using UnityEngine.Events;

public class AfterDialogueCustomEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent afterDialogueEvent;

    public UnityEvent AfterDialogueEvent => afterDialogueEvent;
}
