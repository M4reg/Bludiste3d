using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   [SerializeField] private AudioClip soundtrack; // Zvukový podklad pro hlavní menu
    private AudioSource audioSource;
    void Start()
    {
        // Získání nebo přidání komponenty AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Nastavení a spuštění soundtracku
        if (soundtrack != null)
        {
            audioSource.clip = soundtrack; // Nastaví klip
            audioSource.loop = true; // Smyčka přehrávání
            audioSource.playOnAwake = true; // Přehrát ihned po spuštění scény
            audioSource.Play(); // Spustí hudbu
        }
    }
    // Spuštění hry – načte další scénu v pořadí (např. výběr levelu)
     public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }
    
    // Ukončení hry – funguje jen v buildu, ne v editoru
     public void Quit(){
        Application.Quit();
     }
}
