using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Vector2 TargetPosition;
    public Vector2 CorrectPosition;
    [SerializeField] private Sprite ScarySprite;
    private Sprite NormalSprite;
    private SpriteRenderer TileSpriteRenderer;
    private Color WhiteColour = Color.white;
    private Color BlackColour = Color.black;
    bool fade;
    public bool CorrectPlace;
    public int TileNumber;
    private bool won;
    private void Awake()
    {
        TileSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        NormalSprite = TileSpriteRenderer.sprite;
        TargetPosition = transform.position;
        CorrectPosition = TargetPosition;
    }


    private void Update()
    {
        if (!won)
        {
            transform.position = Vector2.Lerp(transform.position, TargetPosition, 0.05f);
            if (TargetPosition == CorrectPosition)
            {
                //TileSpriteRenderer.color = BlackColour;
                if (!fade)
                {
                    CorrectPlace = true;
                    StopAllCoroutines();
                    fade = true;
                    StartCoroutine(FadeImage(WhiteColour, BlackColour, 2f, ScarySprite));
                }
            }
            else
            {
                if (fade)
                {
                    CorrectPlace = false;
                    StopAllCoroutines();
                    fade = false;
                    TileSpriteRenderer.sprite = NormalSprite;
                    StartCoroutine(FadeImage(BlackColour, WhiteColour, .5f, NormalSprite));
                }
            }
        }
    }
    IEnumerator FadeImage(Color startColor,Color endColor,float fadeDuration,Sprite newsprite)
    {
        float elapsedTime = 0f;

        // Gradually change the color from white to black over fadeDuration seconds
        while (elapsedTime < fadeDuration)
        {
            // Calculate the lerp amount based on elapsed time
            float t = elapsedTime / fadeDuration;

            // Lerp between start and end colors
            TileSpriteRenderer.color = Color.Lerp(startColor, endColor, t);

            // Wait for the next frame
            yield return null;

            // Update elapsed time
            elapsedTime += Time.deltaTime;
        }
        // Ensure the image ends with the black color
        TileSpriteRenderer.color = endColor;
        TileSpriteRenderer.sprite = newsprite;
    }

    public void togglecolour()
    {
        won = true;
        TileSpriteRenderer.sprite = ScarySprite;
        TileSpriteRenderer.color = WhiteColour;
    }
}
