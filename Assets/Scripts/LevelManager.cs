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
        Time.timeScale = 1f;
        collectedDiamonds = 0;
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

        if (finishMenuSound != null)
    {
        finishAudioSource.PlayOneShot(finishMenuSound);
    }
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

        if (gameOverMenuSound != null)
        {
            gameOverAudioSource.PlayOneShot(gameOverMenuSound);
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