using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    public delegate void LightSwitchOnDelegate();
    public static LightSwitchOnDelegate Switchon;
    public delegate void LightSwitchOffDelegate();
    public static LightSwitchOffDelegate Switchoff;

    bool generator;
    private void Update()
    {
        
    }
    void SwitchLightOn()
    {
        if (generator)
        {
            Switchon?.Invoke();
        }
    }
    void SwitchLightOff()
    {
        if (generator)
        {
            Switchoff?.Invoke();
        }
    }
    void togglegenerator()
    {
        generator = true;
    }
}
