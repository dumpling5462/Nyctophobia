using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneLight : MonoBehaviour
{
    Light2D gamelight;
    public delegate void TurnOutsideLightOnDelegate();
    public static TurnOutsideLightOnDelegate OutsideLightOn;
    public delegate void TurnOutsideLightOffDelegate();
    public static TurnOutsideLightOffDelegate OutsideLightOff;

    private void Update()
    {
        LightSwitch.Switchoff += ToggleLightOff;
        LightSwitch.Switchon += ToggleLightOn;
    }
    void Start()
    {
        gamelight = gameObject.GetComponent<Light2D>();
        gamelight.intensity = 0f;
    }
    void ToggleLightOn()
    {
        OutsideLightOn?.Invoke();
        gamelight.intensity = 1f;
    }
    private void ToggleLightOff()
    {
        OutsideLightOff?.Invoke();
        gamelight.intensity = 0f;
    }
    private void OnDestroy()
    {
        LightSwitch.Switchoff -= ToggleLightOff;
        LightSwitch.Switchon -= ToggleLightOn;
    }
}
