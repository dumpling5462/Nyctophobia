using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour, InteractibleInterfaceScript
{
    [SerializeField] private string Prompt;
    string InteractibleInterfaceScript.InteractionPrompt => Prompt;

    private InventoryScript inventory;
    public GameObject itembutton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        PickItemUp();
    //    }
    //}

    bool InteractibleInterfaceScript.Interact(InteractorScript Interactor)
    {
        PickItemUp();
        return true;
    }

    public void PickItemUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.IsFull[i] == false)
            {
                //Item can be added to the inventory
                inventory.IsFull[i] = true;
                Instantiate(itembutton, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
        }
    }
}
