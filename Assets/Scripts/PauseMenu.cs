using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Statická proměnná, která kontroluje, zda je hra pozastavena
    public static bool Paused = false;
    // Odkaz na objekt s pauzovacím menu
    public GameObject PauseMenuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; // Ujistíme se, že hra běží normálně při startu
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Kontrola stisku klávesy Escape
        {
            if (Paused)  // Pokud je hra pozastavena
            {
                Play();  // Pokračujeme ve hře
            } 
            else
            {
                Stop(); // Pozastavíme hru
            }
        }
    }

    // Metoda pro pozastavení hry
    void Stop()
    {
        PauseMenuCanvas.SetActive(true); // Zobrazíme pauzovací menu
        Time.timeScale = 0f; // Pozastavíme čas
        Paused = true; // Nastavíme, že hra je pozastavena
        Cursor.lockState = CursorLockMode.None; // Odemkneme kurzor
        Cursor.visible = true; // Ukážeme kurzor

        // Zastavíme pohyb hráče
        GD_FirstPersonController controller = Object.FindFirstObjectByType<GD_FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = false; // Vypneme ovládání hráče
        }

    }

    // Metoda pro pokračování ve hře 
    public void Play()
    {
        PauseMenuCanvas.SetActive(false); // Skryjeme pauzovací menu
        Time.timeScale = 1f;    // Obnovíme normální čas
        Paused = false; // Nastavíme, že hra není pozastavena
        Cursor.lockState = CursorLockMode.Locked; // Uzamkneme kurzor
        Cursor.visible = false; // Skryjeme kurzor

        // Znovu povolíme pohyb hráče
        GD_FirstPersonController controller = Object.FindFirstObjectByType<GD_FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = true;  // Povolení ovládání hráče
        }
    }

    // Metoda pro přechod do hlavní nabídky
    public void MainMenuButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
