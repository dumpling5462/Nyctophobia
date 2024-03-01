using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractibleInterfaceScript 
{
    public string InteractionPrompt { get; }

    public bool Interact(InteractorScript Interactor);


}
