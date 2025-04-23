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
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerLight = player.GetComponentInChildren<Light>();
        flashlight = player.GetComponentInChildren<Flashlight>();
        GoToRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
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
                            Debug.Log("Enemy sees flashlight!");
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
        Debug.Log("You were caught by the enemy!");
        // Add your game over logic here:
        // Time.timeScale = 0f;
        // SceneManager.LoadScene("GameOver");
    }
}
