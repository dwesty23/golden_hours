using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpriteDisplayController : MonoBehaviour
{
    public GameObject firstSprite;   // Assign your first sprite in the Inspector
    public GameObject secondSprite;  // Assign your second sprite in the Inspector

    void Start()
    {
        // Start with only the first sprite active
        firstSprite.SetActive(true);
        secondSprite.SetActive(false);

        // Start the coroutine to show the second sprite
        StartCoroutine(ShowSecondSpriteAfterDelay(3f));
    }

    IEnumerator ShowSecondSpriteAfterDelay(float delay)
    {
        // Wait for the specified delay (5 seconds)
        yield return new WaitForSeconds(delay);

        // Render the second sprite
        secondSprite.SetActive(true);

        // Wait for another 5 seconds
        yield return new WaitForSeconds(3f);

        // Load the next scene
        SceneManager.LoadScene(1);
    }
}
