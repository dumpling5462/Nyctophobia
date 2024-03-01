using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosePuzzle : MonoBehaviour, InteractibleInterfaceScript
{
    [SerializeField] private TopDownCharacterController topDownCharacterController;
    [SerializeField]private LightningFlash LightningScript;
    [SerializeField]private ToggleFlashlight FlashlightScript;
    [SerializeField] GameObject Puzzle;
    [SerializeField] private string Prompt;
    string InteractibleInterfaceScript.InteractionPrompt => Prompt;

    bool InteractibleInterfaceScript.Interact(InteractorScript Interactor)
    {
        OpenClosePuzzleCanvas();
        return true;
    }

    public void OpenClosePuzzleCanvas()
    {
        if (Puzzle.activeSelf)
        {
            Puzzle.SetActive(false);
            if (!FlashlightScript.InLight)
            {
                FlashlightScript.scarysound.Play();
            }
            topDownCharacterController.enabled = true;
            LightningScript.enabled = true;
            FlashlightScript.enabled = true;
        }
        else
        {
            Puzzle.SetActive(true);
            FlashlightScript.scarysound.Stop();
            topDownCharacterController.enabled = false;
            LightningScript.enabled = false;
            FlashlightScript.enabled = false;
        }
    }
}
