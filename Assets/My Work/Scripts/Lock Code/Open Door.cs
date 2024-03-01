using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour, InteractibleInterfaceScript
{
    [SerializeField] private string Prompt;
    [SerializeField] GameObject Flashlight;
    [SerializeField] GameObject Lightning;
    private LightningFlash LightningScript;
    private ToggleFlashlight FlashlightScript;
    string InteractibleInterfaceScript.InteractionPrompt => Prompt;

    [SerializeField] Canvas DoorCanvas;

    private void Start()
    {
        LightningScript = Lightning.GetComponent<LightningFlash>();
        FlashlightScript = Flashlight.GetComponent<ToggleFlashlight>();
    }
    bool InteractibleInterfaceScript.Interact(InteractorScript Interactor)
    {
        OpenDoorCanvas();
        return true;
    }

    private void OpenDoorCanvas()
    {
        if (DoorCanvas.enabled == false)
        {
            FlashlightScript.scarysound.Stop();
            LightningScript.enabled = false;
            FlashlightScript.enabled = false;
            DoorCanvas.enabled = true;           
        }
        else if (DoorCanvas.enabled == true)
        {
            if (!FlashlightScript.InLight)
            {
                FlashlightScript.scarysound.Play();
            }
            LightningScript.enabled = true;
            FlashlightScript.enabled = true;
            DoorCanvas.enabled = false;
        }
    }
}
