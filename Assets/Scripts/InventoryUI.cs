using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI diamondText;
    void Start()
    {
        diamondText = GetComponent<TextMeshProUGUI>();  // Získání reference na TextMeshProUGUI

        // Debug pro ověření, že máme správně přiřazený TextMeshProUGUI
        if (diamondText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject!");
        }
    }

    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        if (diamondText != null)
        {
            // Aktualizace textu s počtem diamantů
            Debug.Log("Updating diamond text to: " + playerInventory.NumberOfDiamonds);
            diamondText.text = playerInventory.NumberOfDiamonds.ToString();
        }
        else
        {
            Debug.LogError("diamondText is null! Ensure you have a TextMeshProUGUI component on this GameObject.");
        }
    }
}
