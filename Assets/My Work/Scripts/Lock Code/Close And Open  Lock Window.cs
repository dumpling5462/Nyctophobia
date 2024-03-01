using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLockWindow : MonoBehaviour
{
    [SerializeField] GameObject Flashlight;
    [SerializeField] GameObject Lightning;
    private LightningFlash LightningScript;
    private ToggleFlashlight FlashlightScript;
    [SerializeField] Canvas LockCanvas;

    private void Start()
    {
        LightningScript = Lightning.GetComponent<LightningFlash>();
        FlashlightScript = Flashlight.GetComponent<ToggleFlashlight>();
    }
    public void CloseLockCanvas()
    {
        if (!FlashlightScript.InLight)
        {
            FlashlightScript.scarysound.Play();
        }
        LightningScript.enabled = true;
        FlashlightScript.enabled = true;
        LockCanvas.enabled = false;
    }
    public void OpenLockCanvas()
    {

        FlashlightScript.scarysound.Stop();
        LightningScript.enabled = false;
        FlashlightScript.enabled = false;
        LockCanvas.enabled = true;
    }
}
