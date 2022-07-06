using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;
    }

    public event Action onGainFrustration;
    public void GainFrustration()
    {
        if (onGainFrustration != null)
        {
            onGainFrustration();
        }
    }

    public event Action onMoneyCollected;
    public void MoneyCollected()
    {
        if (onMoneyCollected != null)
        {
            onMoneyCollected();
        }
    }

    public event Action onDiamondCollected;
    public void DiamondCollected()
    {
        if (onDiamondCollected != null)
        {
            onDiamondCollected();
        }
    }
}
