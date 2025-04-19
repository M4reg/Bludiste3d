using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.DiamondCollected();
            gameObject.SetActive(false);
        }
    }
}
