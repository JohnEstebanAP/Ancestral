using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerCoins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null) {
            inventory.CoinCollected();
            gameObject.SetActive(false);
        }
    }
}
