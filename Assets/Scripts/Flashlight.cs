using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public GameObject ON;
    public GameObject OFF;
    public bool isOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Přidání proměnných pro zvuky
    public AudioClip flashlightOnSound; // Zvuk zapnutí baterky
    public AudioClip flashlightOffSound; // Zvuk vypnutí baterky
    private AudioSource audioSource;

    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isOn = false;

        // Inicializace AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
                audioSource.PlayOneShot(flashlightOffSound);
            }
            else
            {
                ON.SetActive(true);
                OFF.SetActive(false);
                audioSource.PlayOneShot(flashlightOnSound);
            }
            isOn = !isOn;
        }
    }
}
