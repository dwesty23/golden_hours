using UnityEngine;

public class LightController2D : MonoBehaviour
{
    public GameObject[] lightObjects; // Array to hold GameObjects with Light components

    void Update()
    {
        // Check if the lightObjects array is not null
        if (lightObjects != null)
        {
            // Check the global state of the lights
            if (LightManager.lightsOn)
            {
                // Loop through each GameObject in the array
                for (int i = 0; i < lightObjects.Length; i++)
                {
                    // Enable the light component of the GameObject
                    if (lightObjects[i] != null) // Check if GameObject is not null
                    {
                        lightObjects[i].SetActive(true);
                    }
                }
            }
            else
            {
                // Loop through each GameObject in the array
                for (int i = 0; i < lightObjects.Length; i++)
                {
                    // Disable the light component of the GameObject
                    if (lightObjects[i] != null) // Check if GameObject is not null
                    {
                        lightObjects[i].SetActive(false);
                    }
                }
            }
        }
    }
}
