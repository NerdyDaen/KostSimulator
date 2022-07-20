using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public Animator animator;
    public GameObject PopUp;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  && other.TryGetComponent(out PlayerActive player))
        {
            PopUp.SetActive(true);
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerActive player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                PopUp.SetActive(false);
                player.Interactable = null;
                animator.SetBool("IsTalking", false);
            }
        }
    }

    public void Interact(PlayerActive player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        animator.SetBool("IsTalking", true);
        player.DialogueUI.ShowDialogue(dialogueObject);
        PopUp.SetActive(false);
        player.Interactable = null;
    }
}
