using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenLockedDoorScript : OpenDoorObjectScript
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] string LockPrompt;
    private bool running;
    private bool locked= true;
    private InventoryScript Inventory;

    public override string InteractionPrompt => LockPrompt;

    private void Awake()
    {      
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
    }

    public override bool Interact(InteractorScript Interactor)
    {
        if (locked)
        {
            locked = checklock();
        }
        else if (!locked) 
        {            
        toggledoorstate();
        }
        return true;
    }
    private GameObject currentobject;
    private Slot currentslot;
    private bool checklock()
    {
        LockPrompt = prompt;
        if (locked)
        {
            for (int i = 0; i < Inventory.IsFull.Length; i++)
            {
                currentobject = Inventory.slots[i];
                foreach (Transform child in currentobject.transform)
                {
                    if (child.name.Contains("Key"))
                    {
                        locked = !locked;
                        StartCoroutine(ToggleInteract());
                        StartCoroutine(FlashText("Door Unlocked", .5f));
                        currentslot = Inventory.slots[i].GetComponent<Slot>();
                        currentslot.UseItem();
                        return false;
                    }
                }
            }
        }
        StartCoroutine(FlashText("Cannot Open Door Locked", .5f));
        return true;
    }

    private IEnumerator ToggleInteract()
    {
        DoorCollider.layer = LayerMask.NameToLayer("Tiles");
        yield return new WaitForSeconds(.1f);
        DoorCollider.layer = LayerMask.NameToLayer("Interactible");
    }

    private IEnumerator FlashText(string TextToFlash, float TimePause)
    {
        if (!running)
        {
            Debug.Log("here");
            Text.enabled = true;
            Text.text = TextToFlash;
            StartCoroutine(FadeImage(Text, Color.clear, Color.white, TimePause));
            yield return new WaitForSeconds(TimePause);
            StartCoroutine(FadeImage(Text, Color.white, Color.clear, TimePause));
            yield return new WaitForSeconds(TimePause);
            Text.enabled = false;

        }
        yield return null;
    }
    IEnumerator FadeImage(TextMeshProUGUI Text, Color startColor, Color endColor, float fadeDuration)
    {
        running = true;
        float elapsedTime = 0f;

        // Gradually change the color from white to black over fadeDuration seconds
        while (elapsedTime < fadeDuration)
        {
            // Calculate the lerp amount based on elapsed time
            float t = elapsedTime / fadeDuration;

            // Lerp between start and end colors
            Text.color = Color.Lerp(startColor, endColor, t);

            // Wait for the next frame
            yield return null;

            // Update elapsed time
            elapsedTime += Time.deltaTime;
        }
        // Ensure the image ends with the black color
        Text.color = endColor;
        running = false;
    }
}
