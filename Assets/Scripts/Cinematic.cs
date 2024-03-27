using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    // This method is called when the script instance is being loaded
    void Start()
    {
        // Start the coroutine to wait for 5 seconds before loading scene 2
        StartCoroutine(LoadSceneAfterDelay(5));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Wait for the specified delay duration
        yield return new WaitForSeconds(delay);

        // Load scene 2
        SceneManager.LoadScene(2);
    }

}
