using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestActive : MonoBehaviour
{
    public TextMeshProUGUI storyTitle;
    public TextMeshProUGUI storyDescription;
    public string storyTitleString;
    public string storyDescriptionString;

    public void ChangeText()
    {
        storyTitle.text = storyTitleString;
        storyDescription.text = storyDescriptionString;
    }
}
