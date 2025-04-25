using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   [SerializeField] private AudioClip soundtrack; // Audio clip for the main menu soundtrack
    private AudioSource audioSource;
    void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure AudioSource for soundtrack
        if (soundtrack != null)
        {
            audioSource.clip = soundtrack;
            audioSource.loop = true; // Loop the soundtrack
            audioSource.playOnAwake = true; // Play on scene start
            audioSource.Play(); // Ensure playback starts
        }
    }
    
     public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }

     public void Quit(){
        Application.Quit();
        Debug.Log("Quit Game");
     }
}
