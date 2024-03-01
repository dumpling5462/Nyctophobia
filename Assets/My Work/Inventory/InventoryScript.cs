using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] Canvas inventorycanvas;
    [SerializeField] Canvas BagCanvas;
    public bool[] IsFull;
    public GameObject[] slots;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventorycanvas.enabled == false)
            {
                inventorycanvas.enabled = true;
                BagCanvas.enabled = false;
            }
            else
            {
                inventorycanvas.enabled = false;
                BagCanvas.enabled = true;
            }
        }
    }
}
