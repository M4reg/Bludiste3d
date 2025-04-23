using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;

    private int totalDiamonds;
    private int collectedDiamonds;

    void Start()
    {
        Time.timeScale = 1f;
        collectedDiamonds = 0;
        finishMenu.SetActive(false);
        InitializeDiamondsCount();
    }

    public void OnDiamondCollected()
    {
        collectedDiamonds++;
        var playerInventory = FindFirstObjectByType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.DiamondCollected(); // Volá metodu, která aktualizuje UI
        }
        if (collectedDiamonds >= totalDiamonds)
        {
            ShowFinishMenu();
        }
    }

    private void ShowFinishMenu()
    {
        finishMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Unlock the next level
        GameManager.Instance.UnlockNextLevel();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void InitializeDiamondsCount()
    {
        totalDiamonds = Object.FindObjectsByType<Diamond>(FindObjectsSortMode.None).Length;
        Debug.Log("Diamonds found (after maze generation): " + totalDiamonds);
        // Najdeme hráče a nastavíme mu počet diamantů
        var player = FindFirstObjectByType<PlayerInventory>();
        if (player != null)
        {
            player.TotalDiamondsToCollect = totalDiamonds;
        }
    }

}