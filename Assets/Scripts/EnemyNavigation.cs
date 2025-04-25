using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    public Transform player;
    public float detectionRange = 10f;
    public float killRange = 1.5f;
    public float patrolRadius = 20f;
    public LayerMask flashlightLayer;

    private NavMeshAgent agent;
    private Light playerLight;
    private Flashlight flashlight;
    
    private float patrolTimer = 0f;
    private float patrolDelay = 5f;

    // Proměnné pro zvuk kroků
    public AudioClip footstepSound; // Zvuk kroků
    private AudioSource audioSource;
    public float footstepInterval = 0.5f; // Interval mezi kroky
    private float footstepTimer = 0f;
    public float maxFootstepDistance = 20f; // Maximální vzdálenost, při které je zvuk slyšet
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerLight = player.GetComponentInChildren<Light>();
        flashlight = player.GetComponentInChildren<Flashlight>();
        GoToRandomPoint();

        // Inicializace AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Nastavení AudioSource pro 3D zvuk
        audioSource.clip = footstepSound;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Plně 3D zvuk
        audioSource.maxDistance = maxFootstepDistance; // Maximální vzdálenost slyšitelnosti
        audioSource.rolloffMode = AudioRolloffMode.Linear; // Lineární úbytek hlasitosti
    }

    // Update is called once per frame
    void Update()
    {
        // Přehrávání zvuků kroků, pokud se nepřítel pohybuje
        if (agent.velocity.magnitude > 0.1f) // Kontrola, zda se nepřítel pohybuje
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                audioSource.PlayOneShot(footstepSound);
                footstepTimer = 0f; // Reset časovače
            }
        }
        else
        {
            footstepTimer = footstepInterval; // Reset časovače, když nepřítel stojí
        }
        
        if (CanSeeFlashlight())
        {
            
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) < killRange)
            {
                KillPlayer();
            }
        }
        else
        {
           
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolDelay || agent.remainingDistance < 0.5f)
            {
                GoToRandomPoint();
                patrolTimer = 0f;
            }
        }
    }
    void GoToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
    bool CanSeeFlashlight()
    {
        if (flashlight != null && flashlight.isOn && playerLight != null && playerLight.enabled)
        {
        Vector3 toEnemy = transform.position - playerLight.transform.position;
        float distance = toEnemy.magnitude;

            if (distance <= detectionRange)
            {
                float angle = Vector3.Angle(playerLight.transform.forward, toEnemy.normalized);
                if (angle <= playerLight.spotAngle / 2f)
                {
                    // Je enemy v kuželu světla?
                    if (Physics.Raycast(playerLight.transform.position, toEnemy.normalized, out RaycastHit hit, detectionRange))
                    {
                        if (hit.transform == transform)
                        {
                            return true;
                        }
                    }
                }
            }
        }   
        return false;
    }
    void KillPlayer()
    {
        LevelManager levelManager = Object.FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.ShowGameOverMenu(); // Volá metodu pro zobrazení Game Over menu
        }
    }
}
