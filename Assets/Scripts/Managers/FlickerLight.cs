using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public GameObject[] lightObjects; // Array to hold GameObjects with Light components
    public float minIntensity = 1f; // Minimum intensity of light
    public float maxIntensity = 10f; // Maximum intensity of light
    public float minFlickerSpeed = 0.5f; // Minimum speed of flickering
    public float maxFlickerSpeed = 1.5f; // Maximum speed of flickering

    private float[] flickerSpeeds; // Array to hold the flicker speed for each light

    void Start()
    {
        // Assign a random flicker speed to each light
        flickerSpeeds = new float[lightObjects.Length];
        for (int i = 0; i < lightObjects.Length; i++)
        {
            flickerSpeeds[i] = Random.Range(minFlickerSpeed, maxFlickerSpeed);
        }
    }

    void Update()
    {
        for (int i = 0; i < lightObjects.Length; i++)
        {
            GameObject lightObject = lightObjects[i];
            Light2D light = lightObject.GetComponent<Light2D>();
            if (light != null)
            {
                // Randomly change the intensity of the light
                light.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeeds[i], 1));
            }
        }
    }
}