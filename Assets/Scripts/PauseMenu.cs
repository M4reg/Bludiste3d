using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }
    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GD_FirstPersonController controller = Object.FindFirstObjectByType<GD_FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = false; // Disable the player controller
        }

    }
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GD_FirstPersonController controller = Object.FindFirstObjectByType<GD_FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = true; // Enable the player controller
        }
    }
    public void MainMenuButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
