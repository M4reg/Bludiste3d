using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI diamondText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diamondText = GetComponent<TextMeshProUGUI>();
    }

   
   public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        diamondText.text = playerInventory.NumberOfDiamonds.ToString();
    }
}
