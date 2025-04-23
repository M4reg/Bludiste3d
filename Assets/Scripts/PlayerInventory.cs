using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds { get; private set; }
    public int TotalDiamondsToCollect { get; set; }
    public UnityEvent<PlayerInventory> OnDiamondCollected;
    public InventoryUI inventoryUI;

    public void DiamondCollected()
    {
        NumberOfDiamonds++;

        // Debug pro ověření, že metoda DiamondCollected je volaná
        Debug.Log("Diamond collected! Total: " + NumberOfDiamonds);

        // Pokud máme připojený InventoryUI, aktualizujeme text
        if (inventoryUI != null)
        {
            inventoryUI.UpdateDiamondText(this);  // Voláme metodu pro aktualizaci textu
            Debug.Log("Updated UI with new number of diamonds: " + NumberOfDiamonds);
        }

        OnDiamondCollected.Invoke(this);
    }
}
