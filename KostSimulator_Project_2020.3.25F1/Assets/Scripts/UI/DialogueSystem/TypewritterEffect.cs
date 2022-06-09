using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewritterEffect : MonoBehaviour
{
    [SerializeField] private float typewritterSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.', '!', '?' }, 0.6f), //pause wait time agar ada efek berhenti 
        new Punctuation(new HashSet<char>() {',', ';', ':' }, 0.3f)
    };

    private Coroutine typingCoroutine;
    private TMP_Text textLabel;

    private string textToType;

    public void Run(string textToType, TMP_Text textLabel)
    {
        //typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
        this.textToType = textToType;
        this.textLabel = textLabel;

        typingCoroutine = StartCoroutine(TypeText());
    }

    public void Stop()
    {
        if (!IsRunning) return;

        StopCoroutine(typingCoroutine);
        IsRunning = false;
        OnTypingCompleted();
        
    }
    //private IEnumerator TypeText (string textToType, TMP_Text textLabel)
    private IEnumerator TypeText()
    {
        IsRunning = true;

        textLabel.maxVisibleCharacters = 0;
        textLabel.text = textToType;
        //textLabel.text = string.Empty;

        yield return new WaitForSeconds(0.4f);

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;
            t += Time.deltaTime * typewritterSpeed; //misal t=2.52

            charIndex = Mathf.FloorToInt(t); //charindex = 2
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;
                //textLabel.text = textToType.Substring(0, i + 1);
                textLabel.maxVisibleCharacters = i + 1;
                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        textLabel.maxVisibleCharacters = textToType.Length;
        OnTypingCompleted();
    }

    private void OnTypingCompleted()
    {
        IsRunning = false;
        textLabel.maxVisibleCharacters = textToType.Length;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuantionCategory in punctuations)
        {
            if (punctuantionCategory.Punctuations.Contains(character))
            {
                waitTime = punctuantionCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
