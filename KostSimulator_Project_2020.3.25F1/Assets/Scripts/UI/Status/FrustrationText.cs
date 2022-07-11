using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrustrationText : MonoBehaviour, IDataPersistence
{

    public Slider FrustrationSlider;
    [SerializeField] private int frustrationLevel = 0;

    private TextMeshProUGUI frustrationText;

    private void Awake()
    {
        frustrationText = this.GetComponent<TextMeshProUGUI>();
    }

    public void LoadData (GameData data)
    {
        this.frustrationLevel = data.frustrationLevel;
    }

    public void SaveData(GameData data)
    {
        data.frustrationLevel = this.frustrationLevel;
    }

    private void Start()
    {
        //subscribe to events
        GameEventsManager.instance.onGainFrustration += OnGainFrustration;
    }

    private void OnDestroy()
    {
        //subscribe to events
        GameEventsManager.instance.onGainFrustration -= OnGainFrustration;
    }

    private void OnGainFrustration()
    {
        frustrationLevel += 10;
    }

    private void Update()
    {
        if (frustrationText != null)
        {
            frustrationText.text = "(" + frustrationLevel + "/100)%";
        }
        if (FrustrationSlider != null)
        {
            FrustrationSlider.value = frustrationLevel;
        }
    }
}
