using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorObjectScript : MonoBehaviour, InteractibleInterfaceScript
{
    [SerializeField] float ObjectRotation;
    [SerializeField] protected string prompt;
    [SerializeField] protected GameObject DoorObject;
    [SerializeField] protected GameObject DoorCollider;
    private BoxCollider2D boxCollider;
    private bool IsOpen;
    private bool opening;
    Vector3 CurrentRotation;
    Vector3 NewRotation;
    public virtual string InteractionPrompt => prompt;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public virtual bool Interact(InteractorScript Interactor)
    {

        toggledoorstate();
        return true;
    }

    protected void toggledoorstate()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (IsOpen)
        {
            IsOpen = false;
            CurrentRotation = DoorObject.transform.eulerAngles;
            NewRotation = new Vector3(0,0,CurrentRotation.z-ObjectRotation);
            DoorCollider.layer = LayerMask.NameToLayer("Tiles");
            boxCollider.enabled = false;
            opening = true;
        }
        else
        {
            IsOpen = true;
            CurrentRotation = DoorObject.transform.eulerAngles;
            NewRotation = new Vector3(0,0,CurrentRotation.z+ObjectRotation);
            DoorCollider.layer = LayerMask.NameToLayer("Tiles");
            boxCollider.enabled = false;
            opening = true;
        }
    }

    private void Update()
    {
        if (opening)
        {
            DoorObject.transform.rotation = Quaternion.Slerp(DoorObject.transform.rotation, Quaternion.Euler(NewRotation), (5f * Time.deltaTime));
            if (Quaternion.Angle(DoorObject.transform.rotation, Quaternion.Euler(NewRotation)) < 0.1f)
            {
                DoorObject.transform.rotation = Quaternion.Euler(NewRotation);
                DoorCollider.layer = LayerMask.NameToLayer("Interactible");
                boxCollider.enabled = true;
                opening = false;
            }
        }
    }
}
