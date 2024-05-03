using UnityEngine;
using UnityEngine.Rendering;

public class DayNightScript : MonoBehaviour
{
    public Volume ppv;
    public float cycleDuration = 20.0f; // Duration of one complete cycle in seconds
    public float lightsToggleThreshold = 0.5f; // The threshold time within the cycle to turn the lights on
    
    void Update()
    {
        // Calculate the current time within one cycle
        float timeWithinCycle = Mathf.Repeat(Time.time, cycleDuration) / cycleDuration;
        // float totalTime = GlobalTimer.Instance.totalTime;
        // float timeWithinCycle = Mathf.Repeat(GlobalTimer.Instance.GetTimePassed(), totalTime) / totalTime;
        // float timeWithinDay = Mathf.Repeat(globalTimer.timePassed, globalTimer.totalTime / 3) / (globalTimer.totalTime / 3);

        // Map the time within the cycle to the ppv.weight parameter
        ppv.weight = Mathf.Sin(timeWithinCycle * Mathf.PI); // Using a sine wave for smooth oscillation

        // Check if it's time to turn the lights on or off
        if (ppv.weight > lightsToggleThreshold)
        {
            // Turn the lights on
            LightManager.lightsOn = true;
        }
        else
        {
            // Turn the lights off
            LightManager.lightsOn = false;
        }
    }
}
