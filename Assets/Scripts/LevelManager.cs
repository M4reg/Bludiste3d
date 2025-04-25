using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private AudioClip finishMenuSound; 
    [SerializeField] private AudioClip gameOverMenuSound;
    private AudioSource finishAudioSource;
    private AudioSource gameOverAudioSource;

    private AudioSource audioSource;
    private int totalDiamonds;
    private int collectedDiamonds;

    void Start()
    {
        // Obnovení hry na začátku
        Time.timeScale = 1f;
        collectedDiamonds = 0;
        // Skryjeme menu (jsou aktivní jen při výhře/prohře)
        finishMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        InitializeDiamondsCount();

        // Finish zvuk
        finishAudioSource = gameObject.AddComponent<AudioSource>();
        finishAudioSource.volume = 0.55f; 

        // GameOver zvuk
        gameOverAudioSource = gameObject.AddComponent<AudioSource>();
        gameOverAudioSource.volume = 0.016f; 
    }

    // Zavoláno, když hráč sebere diamant
    public void OnDiamondCollected()
    {
        collectedDiamonds++;

        var playerInventory = FindFirstObjectByType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.DiamondCollected(); // Volá metodu, která aktualizuje UI
        }
        // Pokud sebrány všechny diamanty, vyhrál
        if (collectedDiamonds >= totalDiamonds)
        {
            ShowFinishMenu();
        }
    }
    // Zobrazí výherní menu
    private void ShowFinishMenu()
    {
        finishMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Odemkne další úroveň
        GameManager.Instance.UnlockNextLevel();

        if (finishMenuSound != null)
    {
        finishAudioSource.PlayOneShot(finishMenuSound);
    }
    }

     // Zobrazí game over menu
    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Vypnutí pohybu hráče
        GD_FirstPersonController controller = Object.FindFirstObjectByType<GD_FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = false; 
        }

        if (gameOverMenuSound != null)
        {
            gameOverAudioSource.PlayOneShot(gameOverMenuSound);
        }
    }
    // Restartuje aktuální scénu
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Načte hlavní menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    // Spočítá diamanty ve scéně a nastaví je hráči
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