using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TriggerText;
    [SerializeField] TextMeshProUGUI TextToUpdate;
    [SerializeField] string[] TextChanges;
    [SerializeField] float[] FontSizeChanges;
    private bool running;
    int counter =-1;
    private void Start()
    {
        StartCoroutine(FadeImage(TriggerText, Color.black, TriggerText.color, .5f));
        StartCoroutine(FadeImage(TextToUpdate, Color.black, Color.white, .5f));
    }
    public void clicked()
    {
        if (!running) {
            counter++;
            if (counter == 0)
            {
                StartCoroutine(FadeImage(TriggerText, TriggerText.color, Color.black, 1f));
                //TriggerText.enabled = false;
            }
            if (counter == TextChanges.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                StartCoroutine(FadeImage(TextToUpdate, Color.white, Color.black, 1f));
                StartCoroutine(FadeIn());
            }
        }
    }
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f);
        TextToUpdate.text = TextChanges[counter];
        TextToUpdate.fontSize = FontSizeChanges[counter];
        StartCoroutine(FadeImage(TextToUpdate, Color.black, Color.white, 1f));
    }
    IEnumerator FadeImage(TextMeshProUGUI Text,Color startColor, Color endColor, float fadeDuration)
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
