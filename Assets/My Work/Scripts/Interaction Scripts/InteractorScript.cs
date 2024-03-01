using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorScript : MonoBehaviour
{
    [SerializeField] private Transform InteractionPoint;
    [SerializeField] private float InteractionPointRadius=0.5f;
    [SerializeField] private LayerMask InteractableMask;
    [SerializeField] private InteractionUIScript InteractionPopup;

    private readonly Collider2D[] Colliders = new Collider2D[3];
    [SerializeField] private int NumFound;

    private InteractibleInterfaceScript TempInteractible;

    private void Update()
    {
        NumFound = Physics2D.OverlapCircleNonAlloc(InteractionPoint.position, InteractionPointRadius, Colliders, InteractableMask);
        if (NumFound > 0)
        {
            TempInteractible = Colliders[0].GetComponent<InteractibleInterfaceScript>();
            if (TempInteractible != null)
            {
                if (!InteractionPopup.IsDisplayed)
                {
                    InteractionPopup.SetUp(TempInteractible.InteractionPrompt);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TempInteractible.Interact(this);
                }
            }
        }
        else
        {
            if (TempInteractible != null)
            {
                TempInteractible = null;
            }
            if (InteractionPopup.IsDisplayed)
            {
                InteractionPopup.CloseInteractionCanvas();
            }
        }

    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractionPoint.position,InteractionPointRadius);
    }
}
