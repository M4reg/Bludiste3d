using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something hit the diamond: " + other.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log("Player hit the diamond!");

        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnDiamondCollected();
        }

        gameObject.SetActive(false);
    }
    }
}
