using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private Image speakerImageHolder;
    [SerializeField] private Button buttonNextDialogue;
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] private GameObject triangleNext;
    [SerializeField] public string? nextScene;
    //[SerializeField] private DialogueObject testDialogue;

    public bool IsOpen { get; private set; }

    private bool IsClicked = false;
    private TypewritterEffect typewritterEffect;
    private ResponseHandler responseHandler;

    private void Awake()
    {
        IsClicked = false;
    }

    private void Start()
    {
        typewritterEffect = GetComponent<TypewritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        triangleNext?.SetActive(false);
        IsClicked = false;
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        string speaker = dialogueObject.speaker;
        speakerName.text = speaker;
        if (dialogueObject.imageSpeaker != null)
        {
            speakerImageHolder.gameObject.SetActive(true);
            speakerImageHolder.sprite = dialogueObject.imageSpeaker;
        }
        else speakerImageHolder.gameObject.SetActive(false);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++){
            string dialogue = dialogueObject.Dialogue[i];

            triangleNext.SetActive(false);
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            triangleNext.SetActive(true);
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Space) || IsClicked == true);
            IsClicked = false;
            buttonAudio.Play();
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
            triangleNext.gameObject.SetActive(false);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewritterEffect.Run(dialogue, textLabel);

        while (typewritterEffect.IsRunning)
        {
            yield return null;

            if (IsClicked == true)
            {
                typewritterEffect.Stop();
                IsClicked = false;
            }
        }
    }

    public void CloseDialogueBox()
    {
        IsClicked = false;
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        speakerName.text = string.Empty;
    }

    public void OnClick()
    {
        IsClicked = true;
    }
}
