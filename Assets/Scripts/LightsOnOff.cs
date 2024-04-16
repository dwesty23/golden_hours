using UnityEngine;

public class LightController2D : MonoBehaviour
{
    void Update()
    {
        print("LightController2D.Update()");
        // Check the global state of the lights
        if (LightManager.lightsOn)
        {
            // Enable the GameObject
            gameObject.SetActive(true);
        }
        else
        {
            // Disable the GameObject
            gameObject.SetActive(false);
        }
    }
}
