using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public GameObject ON;
    public GameObject OFF;
    public bool isOn;
    
    // Přidání proměnných pro zvuky
    public AudioClip flashlightOnSound; // Zvuk zapnutí baterky
    public AudioClip flashlightOffSound; // Zvuk vypnutí baterky
    private AudioSource audioSource;

    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isOn = false;

        // Získá nebo vytvoří AudioSource komponentu pro přehrávání zvuků
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
        // Přepínání baterky pomocí klávesy F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                // Vypnutí baterky
                ON.SetActive(false); // Vypne světlo
                OFF.SetActive(true); // Zapne model baterky bez světla
                audioSource.PlayOneShot(flashlightOffSound); // Přehraje zvuk vypnutí
            }
            else
            {
                ON.SetActive(true);
                OFF.SetActive(false);
                audioSource.PlayOneShot(flashlightOnSound);
            }
            isOn = !isOn;  // Přepne stav
        }
    }
}
