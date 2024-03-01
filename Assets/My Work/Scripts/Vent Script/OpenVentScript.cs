using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OpenVentScript : MonoBehaviour
{
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Screws[] Screwsin;
    [SerializeField] private Sprite newsprite;
    [SerializeField] private Image spriterenderer;
    [SerializeField] private GameObject Objecttospawn;
    [SerializeField] private GameObject Objecttospawn2;
    [SerializeField] private TextMeshProUGUI Text;
   
    private bool running;
    private bool spawned;
    private int screwedout;

    public void clicked()
    {
        if (!spawned)
        {
            screwedout = 0;
            foreach (var s in Screwsin)
            {
                if (s.Unscrewed)
                {
                    screwedout++;
                }
            }
            if (screwedout >= Screwsin.Length)
            {
                StopAllCoroutines();
                spawned = true;
                spriterenderer.sprite = newsprite;
                Instantiate(Objecttospawn,spawnpoint.transform,false);
                Instantiate(Objecttospawn2, spawnpoint.transform, false);
            }
            else
            {
                StartCoroutine(FlashText("Take Screws Out To Open",.5f));
            }
        }
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
