using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondCollectedText : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int totalDiamond = 0;

    private int diamondCollected = 0;
    private TextMeshProUGUI diamondCollectedText;

    private void Awake()
    {
        diamondCollectedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameEventsManager.instance.onDiamondCollected += OnDiamondCollected;
    }

    public void LoadData(GameData data)
    {
        foreach(KeyValuePair<string, bool> pair in data.diamondCollected)
        {
            if (pair.Value)
            {
                diamondCollected++;
            }
        }
    }

    public void SaveData(GameData data)
    {
        //no data needed to be saved for this script
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.onDiamondCollected -= OnDiamondCollected;
    }

    private void OnDiamondCollected()
    {
        diamondCollected++;
    }

    private void Update()
    {
        diamondCollectedText.text = diamondCollected.ToString();
    }
}
