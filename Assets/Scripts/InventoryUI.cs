using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    
    private TextMeshProUGUI diamondText;
    public AudioClip diamondCollectSound; // Zvuk pro sbírání diamantů
    private AudioSource audioSource;

    void Start()
    {
        diamondText = GetComponent<TextMeshProUGUI>();  // Získání reference na TextMeshProUGUI
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        if (diamondText != null)
        {
            // Aktualizace textu s počtem diamantů
            diamondText.text = playerInventory.NumberOfDiamonds.ToString();

            // Přehrání zvuku sběru diamantu
            if (diamondCollectSound != null)
            {
                // Najde pozici hráče (pro ladění nebo 3D zvuk)
                Vector3 playerPosition = FindFirstObjectByType<GD_FirstPersonController>().transform.position;
                audioSource.PlayOneShot(diamondCollectSound);
            }
        }
    }
}
