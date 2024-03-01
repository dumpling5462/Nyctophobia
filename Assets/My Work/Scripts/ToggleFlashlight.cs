using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ToggleFlashlight : MouseFlashlightScript
{
    [SerializeField] public AudioSource scarysound;
    [SerializeField] AudioDistortionFilter filter;
    float soundvolume = .1f;
    bool soundon;

    Light2D FlashlightObject;
    public float MaxBatteryLife;
    [SerializeField] float BatteryDrainPercent;
    [SerializeField] float BatteryGainPercent;
    public float CurrentBatteryLife;
    float LightIntensity=1f;

    public bool FlashlightOn;
    public bool InLight;
    public bool OutsideLight;

    bool blinkflash;

     public float DeathTimer;
     public float DeathCounter;
    void Awake()
    {
        FlashlightObject = GetComponent<Light2D>();
        CurrentBatteryLife = MaxBatteryLife;

        LightningFlash.OutsideLightOn += SetOutsideLightOn;
        LightningFlash.OutsideLightOff += SetOutsideLightOff;
        SceneLight.OutsideLightOn += SetOutsideLightOn;
        SceneLight.OutsideLightOff += SetOutsideLightOff;
        BatteryPickUp.AddbatteryLife += addbatterylife;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            addbatterylife();
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            CurrentBatteryLife -= 10f;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleflashlight();
        }
        CheckLight();
        if (FlashlightOn)
        {
            FollowMouse();
            UpdateFlashLight();
        }
        CheckDead();
    }
    void toggleflashlight()
    {
        if (FlashlightOn)
        {
            FlashlightOn = false;
            StopAllCoroutines();
            blinkflash = false;
            FlashlightObject.intensity = 0f;
            if (!OutsideLight)
            {
                InLight = false;
            }
        }
        else if (!FlashlightOn && CurrentBatteryLife >0f)
        {
            FlashlightOn = true;
            FlashlightObject.intensity = LightIntensity;
            if (!OutsideLight)
            {
                InLight = true;
            }
        }
    }
    void CheckLight()
    {
        if (CurrentBatteryLife<=0f)
        {
            if (FlashlightOn)
            {
                FlashlightOn = false;
                StopAllCoroutines();
                blinkflash = false;
                FlashlightObject.intensity = 0f;
            }
        }
        if (OutsideLight && !InLight)
        {
            InLight = true;
        }
        else if (!OutsideLight && InLight && !FlashlightOn)
        {
            InLight = false;
        }
    }
    void UpdateFlashLight()
    {
        if (CurrentBatteryLife > 0f)
        {
            CurrentBatteryLife -= (BatteryDrainPercent * 1 * Time.deltaTime);
            if (!blinkflash)
            {
                FlashlightObject.intensity = LightIntensity;
            }
            if (CurrentBatteryLife > MaxBatteryLife/2)
            {
                LightIntensity = CurrentBatteryLife / MaxBatteryLife;
            }
            else
            {
                LightIntensity = .5f;
            }
            if (CurrentBatteryLife <= MaxBatteryLife * 0.3)
            {
                if (!blinkflash)
                {
                    StartCoroutine(BlinkFlashlight());
                }
            }
            else if (blinkflash && CurrentBatteryLife >= MaxBatteryLife * 0.3)
            {
                StopAllCoroutines();
                blinkflash = false;
            }
        }
        else
        {
            FlashlightObject.intensity = 0f;
            FlashlightOn=false;
            CheckLight();
        }
    }
    public void addbatterylife()
    {
        if(CurrentBatteryLife + (MaxBatteryLife*BatteryGainPercent) <= MaxBatteryLife)
        {
            CurrentBatteryLife += (MaxBatteryLife*BatteryGainPercent);
        }
        else
        {
            CurrentBatteryLife = MaxBatteryLife;
        }
    }

    public void losebatterylife(float losspercent)
    {
        if (CurrentBatteryLife - (MaxBatteryLife * losspercent) >= 0)
        {
            CurrentBatteryLife -= (MaxBatteryLife * losspercent);
        }
        else
        {
            CurrentBatteryLife = 0;
        }
    }
    public void SetOutsideLightOff()
    {
        OutsideLight = false;
    }
    public void SetOutsideLightOn()
    {
        OutsideLight = true; 
    }
    IEnumerator BlinkFlashlight()
    {
        blinkflash = true;
        for (int i = 0; i < UnityEngine.Random.Range(1, 10); i++)
        {
            FlashlightObject.intensity = 0f;
            yield return new WaitForSeconds(UnityEngine.Random.Range(.1f,.5f));
            FlashlightObject.intensity = LightIntensity;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
        }
        blinkflash = false;
    }
    void CheckDead()
    {
        if (!InLight)
        {
            if (!soundon)
            {
                scarysound.volume = soundvolume;
                scarysound.Play();
                soundon = true;
            }
            DeathCounter += (1f * Time.deltaTime);
            soundvolume += (.1f * Time.deltaTime);
            if (soundvolume >= .1f)
            {
                scarysound.volume = soundvolume;
            }
            if (soundvolume > .95f)
            {
                filter.distortionLevel = .95f;
            }
            else
            {
                filter.distortionLevel = soundvolume;
            }
        }
        else if (InLight)
        {
            if (soundon)
            {
                scarysound.Stop();
                soundon = false;
                soundvolume = .1f;
                filter.distortionLevel = 0f;
                DeathCounter = 0f;
            }
        }
        if (DeathCounter>= DeathTimer)
        {
            endgame();
        }
    }
    void endgame()
    {
        SceneManager.LoadScene("DeadScene");
        Debug.Log("DEAD");
    }

    private void OnDestroy()
    {
        LightningFlash.OutsideLightOn -= SetOutsideLightOn;
        LightningFlash.OutsideLightOff -= SetOutsideLightOff;
        SceneLight.OutsideLightOn -= SetOutsideLightOn;
        SceneLight.OutsideLightOff -= SetOutsideLightOff;
        BatteryPickUp.AddbatteryLife -= addbatterylife;
    }
}
