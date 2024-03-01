using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventoryScript : MonoBehaviour
{
    [SerializeField] Canvas inventorycanvas;
    [SerializeField] Canvas BagCanvas;
    public void OpenInventory()
    {
        inventorycanvas.enabled = true;
        BagCanvas.enabled = false;
    }
}
