using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Audio;
using System.Runtime.InteropServices;

public class LightningFlash : MonoBehaviour
{
    [SerializeField] Light2D GlobalLight;
    [SerializeField] AudioSource LightningSound;
    [SerializeField] AudioDistortionFilter LightningDistortion;
    //How dark the light intensity will be
    [SerializeField] float DarkIntensity;
    //how bright the lightning strikes will make the scene
    [SerializeField] float LightIntensity;
    [SerializeField] float LightningTimer;
    //Script uses either set timer, or random range based off boolean
    [SerializeField] bool RandomInterval;
    //Min and Max range for lightning strike intervals
    [SerializeField] float MaxTime;
    [SerializeField] float MinTime;
    //How long the scene will be lit up for
    [SerializeField] float LightningDuration;
    //Tracked time between flashes
    public float Timer;
    //Counts time in the scene
    public float Counter;

    public delegate void TurnOutsideLightOnDelegate();
    public static TurnOutsideLightOnDelegate OutsideLightOn;
    public delegate void TurnOutsideLightOffDelegate();
    public static TurnOutsideLightOffDelegate OutsideLightOff;
    void Start()
    {
        //set the light intensity
        GlobalLight.intensity = DarkIntensity;
        //calls for lightning Strike to occur
        LightningStrike();
        Timer = RandomNumGenerator(RandomInterval,MinTime,MaxTime,Timer);
    }

    void Update()
    {
        //checks to see if the counter is equal to the timer
        if(Counter >= Timer)
        {
            LightningStrike();
            //sets up the timer
            Timer = RandomNumGenerator(RandomInterval, MinTime, MaxTime, Timer);
            //resets the counter
            Counter = 0f;
        }
        else
        {
            //increments the counter, time.deltatime keeps the timer consistsent
            Counter += (1*Time.deltaTime);
        }
    }

    float RandomNumGenerator(bool RandomInt,float MinValue, float MaxValue,float timer)
    {
        //checks if the script uses random intervals
        if (RandomInt)
        {
            //generates a timer within the range
            timer = Random.Range(MinValue, MaxValue);
        }
        else
        {
            //sets timer to the set value
            timer = LightningTimer;
        }
        //returns the timer value
        return timer;
    }
    void LightningStrike()
    {
        //calls for a delay using the coroutine function to allow the lightning to flash
        StartCoroutine(delay(LightningDuration));
    }

    void PlayLightningSound()
    {
        //Plays sound while edting volume and distortion of the sound
        LightningDistortion.distortionLevel = Random.Range(0f, 0.65f);
        LightningSound.volume = Random.Range(0.5f, 1f);
        LightningSound.Play();
    }

    IEnumerator delay(float delaytime)
    {
        //calls play sound to play the audio for the lightning
        PlayLightningSound();
        OutsideLightOn?.Invoke();
        //sets the light intensity to 1 to light up the scene
        GlobalLight.intensity = LightIntensity;
        //calls a delay so the scene can be lit up for a set time
        yield return new WaitForSeconds(delaytime);
        //sets the light intensity back to 0 to make the scene dark
        GlobalLight.intensity = DarkIntensity;
        OutsideLightOff?.Invoke();

    }
}
