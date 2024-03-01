using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Screws : MonoBehaviour
{
    [SerializeField] GameObject Screw;
    [SerializeField] TextMeshProUGUI Text;
    private InventoryScript Inventory;
    public bool Unscrewed;
    bool HasScrewdriver;
    bool running;
    float rotation;
    private Vector3 newrotation;
    private GameObject currentobject;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
    }

    void Update()
    {
        if (HasScrewdriver && !Unscrewed)
        {
            unscrewAnimation();
            if (rotation >= 720)
            {
                Screw.SetActive(false);
                Unscrewed = true;
            }
        }
    }

    public void clicked()
    {
        if (!HasScrewdriver && !Unscrewed)
        {
            for (int i = 0; i < Inventory.IsFull.Length; i++)
            {
                currentobject = Inventory.slots[i];
                foreach (Transform child in currentobject.transform)
                {
                    if (child.name.Contains("ScrewDriver"))
                    {
                        StopAllCoroutines();
                        HasScrewdriver = true;
                        break;
                    }
                }
                if (HasScrewdriver)
                {
                    StopAllCoroutines();
                    break;
                }
            }
            if (!HasScrewdriver)
            {
                StartCoroutine(FlashText("No Screwdriver Found", .5f));
            }
        }
    }

    void unscrewAnimation()
    {
        newrotation = Screw.transform.eulerAngles;
        newrotation += new Vector3(0,0,1);
        Screw.transform.eulerAngles = newrotation;
        Screw.transform.localScale = new Vector3(Screw.transform.localScale.x+0.001f,Screw.transform.localScale.y+0.001f,1);
        rotation += 1;
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
