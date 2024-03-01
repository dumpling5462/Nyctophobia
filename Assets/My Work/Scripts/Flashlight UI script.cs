using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlashlightUIscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject flashlightobject;
    [SerializeField] UnityEngine.UI.Image flashlightimage;
    [SerializeField] UnityEngine.UI.Slider flashlightslider;
    [SerializeField] Sprite flaslighton;
    [SerializeField] Sprite flaslightoff;
    [SerializeField] UnityEngine.UI.Slider deathslider;
    ToggleFlashlight flashlight;
    bool flashlightonsp;
    bool flashlightoffsp;
    void Start()
    {
        flashlight = flashlightobject.GetComponent<ToggleFlashlight>();
    }

    // Update is called once per frame
    void Update()
    {
        flashlightslider.value = flashlight.CurrentBatteryLife / flashlight.MaxBatteryLife;
        deathslider.value = flashlight.DeathCounter / flashlight.DeathTimer;
        if (flashlight.FlashlightOn && !flashlightonsp)
        {
            flashlightonsp = true;
            flashlightoffsp = false;
            flashlightimage.sprite = flaslighton;
        }
        else if (!flashlight.FlashlightOn && !flashlightoffsp)
        {
            flashlightonsp = false;
            flashlightoffsp = true;
            flashlightimage.sprite = flaslightoff;
        }
    }
}
