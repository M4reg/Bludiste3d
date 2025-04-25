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
        // Debug pro ověření, že máme správně přiřazený TextMeshProUGUI
        if (diamondText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject!");
        }
        // Kontrola přiřazení zvuku
        if (diamondCollectSound == null)
        {
            Debug.LogWarning("DiamondCollectSound is not assigned in InventoryUI!");
        }
    }

    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        if (diamondText != null)
        {
            // Aktualizace textu s počtem diamantů
            Debug.Log("Updating diamond text to: " + playerInventory.NumberOfDiamonds);
            diamondText.text = playerInventory.NumberOfDiamonds.ToString();

            // Přehrání zvuku sběru diamantu
            if (diamondCollectSound != null)
            {
                // Přehrát zvuk na pozici hráče (pro 3D efekt)
                Vector3 playerPosition = FindFirstObjectByType<GD_FirstPersonController>().transform.position;
                audioSource.PlayOneShot(diamondCollectSound);
                Debug.Log("Playing diamond collect sound at player position: " + playerPosition);
            }
            else
            {
                Debug.LogWarning("Cannot play diamond collect sound: AudioClip is missing in InventoryUI!");
            }
        }
        else
        {
            Debug.LogError("diamondText is null! Ensure you have a TextMeshProUGUI component on this GameObject.");
        }
    }
}
