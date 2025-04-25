using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
    if (other.CompareTag("Player"))
    {
        // Najde instanci třídy LevelManager (správce úrovně)
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            // Pokud správce úrovně existuje, spustí metodu pro sbírání diamantů
            levelManager.OnDiamondCollected();
        }
        // Deaktivuje objekt diamantu (zmizí ze scény)
        gameObject.SetActive(false);
    }
    }
}
