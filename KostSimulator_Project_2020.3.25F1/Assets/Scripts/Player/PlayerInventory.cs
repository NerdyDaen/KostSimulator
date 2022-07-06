using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public GameObject DiamondUI;

    public int NumberOfDiamonds { get; private set; }
    public UnityEvent<PlayerInventory> OnDiamondCollected;
    
    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        OnDiamondCollected.Invoke(this);
        
        //DiamondUI.SetActive(true);
        //Delay();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        DiamondUI.SetActive(false);
    }
}
