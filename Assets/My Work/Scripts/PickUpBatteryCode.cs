using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    public delegate void AddbatteryLifeDelegate();
    public static AddbatteryLifeDelegate AddbatteryLife;
    private Slot itemslot;
    public void BatteryUsed()
    {
        AddbatteryLife?.Invoke();
        itemslot = gameObject.GetComponentInParent<Slot>();
        itemslot.UseItem();
        Destroy(gameObject);
    }
}
