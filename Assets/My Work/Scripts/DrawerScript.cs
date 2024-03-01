using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour,InteractibleInterfaceScript
{
    [SerializeField] private string prompt;
    [SerializeField] private SpriteRenderer sprtRenderer;
    [SerializeField] private Sprite newsprite;
    [SerializeField] private Sprite oldsprite;
    [SerializeField] private GameObject spawnedobject;
    public string InteractionPrompt => prompt;
    private bool done;

    public bool Interact(InteractorScript Interactor)
    {
        toggleopendrawer();
        return true;
    }

    public void toggleopendrawer()
    {
        if (sprtRenderer.sprite == oldsprite)
        {
            StartCoroutine(togglelayer(gameObject));
            prompt = "Close Draw";
            if (spawnedobject != null)
            {
                StartCoroutine(togglelayer(spawnedobject));
                spawnedobject.SetActive(true);
            }
            sprtRenderer.sprite = newsprite;
        }
        else
        {
            StartCoroutine(togglelayer(gameObject));
            prompt = "Open Draw";
            if (spawnedobject != null)
            {
                spawnedobject.layer = LayerMask.NameToLayer("Tiles");
                spawnedobject.SetActive(false);
            }
            sprtRenderer.sprite = oldsprite;
        }
    }

    private void Update()
    {
        if (spawnedobject != null)
        {
            if (sprtRenderer.sprite == newsprite)
            {
                gameObject.layer = LayerMask.NameToLayer("Tiles");
            }
        }
        if (spawnedobject == null && !done)
        {
            done = true;
            StartCoroutine(togglelayer(gameObject));
        }
    }

    private IEnumerator togglelayer(GameObject objecttoggle)
    {
        objecttoggle.layer = LayerMask.NameToLayer("Tiles");
        yield return new WaitForSeconds(.1f);
        objecttoggle.layer = LayerMask.NameToLayer("Interactible");
    }


}
