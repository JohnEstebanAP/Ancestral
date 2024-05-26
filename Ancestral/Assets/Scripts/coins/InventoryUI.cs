using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI textConiCount;
    // Start is called before the first frame update
    void Start()
    {
        textConiCount = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDiamondText(PlayerInventory playerInventory) {
        textConiCount.text = playerInventory.NumberOfDiamonds.ToString();
    }
}
