using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{

    // Počet nasbíraných diamantů
    public int NumberOfDiamonds { get; private set; }

    // Celkový počet diamantů k sebrání v úrovni
    public int TotalDiamondsToCollect { get; set; }

    // Event, který se spustí, když hráč nasbírá diamant
    public UnityEvent<PlayerInventory> OnDiamondCollected;

     // Odkaz na UI inventáře
    public InventoryUI inventoryUI;

     // Metoda pro sbírání diamantů
    public void DiamondCollected()
    {
        // Zvětšení počtu diamantů
        NumberOfDiamonds++;

        // Pokud máme přiřazené UI pro inventář, aktualizujeme zobrazení počtu diamantů
        if (inventoryUI != null)
        {
            inventoryUI.UpdateDiamondText(this);  // Volání metody pro aktualizaci textu
        }

        OnDiamondCollected.Invoke(this); // Vyvolání eventu, že diamant byl nasbírán
    }
}
