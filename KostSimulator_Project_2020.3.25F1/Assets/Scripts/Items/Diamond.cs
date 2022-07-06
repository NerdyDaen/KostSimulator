using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool collected = false;

    public void LoadData(GameData data)
    {
        data.diamondCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.diamondCollected.ContainsKey(id))
        {
            data.diamondCollected.Remove(id);
        }
        data.diamondCollected.Add(id, collected);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null && !collected)
        {
            GameEventsManager.instance.DiamondCollected();
            playerInventory.DiamondCollected();
            collected = true;
            gameObject.SetActive(false);
        }
    }
}
