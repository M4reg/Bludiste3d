using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private GameObject gameOverMenu;

    private int totalDiamonds;
    private int collectedDiamonds;

    void Start()
    {
        Time.timeScale = 1f;
        collectedDiamonds = 0;
        finishMenu.SetActive(false);
        gameOverMenu.SetActive(false);
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
        GameManager.Instance.UnlockNextLevel();
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GD_FirstPersonController controller = Object.FindFirstObjectByType<GD_FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = false; 
        }
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
        // Najdeme hráče a nastavíme mu počet diamantů
        var player = FindFirstObjectByType<PlayerInventory>();
        if (player != null)
        {
            player.TotalDiamondsToCollect = totalDiamonds;
        }
    }

}