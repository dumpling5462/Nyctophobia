using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUIScript : MonoBehaviour
{
    [SerializeField] Canvas InteractionCanvas;
    [SerializeField] TMP_Text InteractionPrompt;
    void Start()
    {
        InteractionCanvas.enabled = false;
    }

    public bool IsDisplayed = false;

    public void SetUp(string PromptText)
    {
        InteractionPrompt.text = PromptText;
        InteractionCanvas.enabled = true;
        IsDisplayed = true;
    }

    public void CloseInteractionCanvas()
    {
        IsDisplayed = false;
        InteractionCanvas.enabled = false;
    }
}
