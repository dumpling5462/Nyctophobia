using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LockScript : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite lockgemsprite;
    [SerializeField] string gemname;

    private InventoryScript inventory;
    private GameObject currentobject;
    private Slot currentslot;
    public bool GemPlaced;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
    }
    public void PlaceGem()
    {
        if (!GemPlaced)
        {
            for (int i = 0; i < inventory.IsFull.Length; i++)
            {
                currentobject = inventory.slots[i];
                foreach(Transform child in currentobject.transform)
                {
                    if (child.name.Contains(gemname))
                    {
                        GemPlaced = true;
                        gem(i);
                        break;
                    }
                }
                if (GemPlaced)
                {
                    break;
                }
            }
        }
    }

    void gem(int i)
    {
        image.sprite = lockgemsprite;
        currentslot = inventory.slots[i].GetComponent<Slot>();
        currentslot.UseItem();
    }
}
