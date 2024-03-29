using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpriteDisplayController : MonoBehaviour
{
    public GameObject firstSprite;   // Assign your first sprite in the Inspector
    public GameObject secondSprite;  // Assign your second sprite in the Inspector
    public GameObject thirdSprite;   // Assign your third sprite in the Inspector
    public GameObject fourthSprite;  // Assign your fourth sprite in the Inspector
    public GameObject fifthSprite;   // Assign your fifth sprite in the Inspector

    void Start()
    {
        // Start with only the first sprite active
        firstSprite.SetActive(true);
        secondSprite.SetActive(false);
        thirdSprite.SetActive(false);
        fourthSprite.SetActive(false);
        fifthSprite.SetActive(false);

        // Start the coroutine to show the second sprite
        StartCoroutine(ShowSecondSpriteAfterDelay(5f));
        StartCoroutine(ShowThirdSpriteAfterDelay(10f));
        StartCoroutine(ShowFourthSpriteAfterDelay(15f));
        StartCoroutine(ShowFifthSpriteAfterDelay(20f));
    }

    IEnumerator ShowSecondSpriteAfterDelay(float delay)
    {
        // Wait for the specified delay (5 seconds)
        yield return new WaitForSeconds(delay);

        // Render the second sprite
        firstSprite.SetActive(false);
        secondSprite.SetActive(true);

        // Wait for the second sprite to become active
        yield return new WaitUntil(() => secondSprite.activeInHierarchy);
    }

    IEnumerator ShowThirdSpriteAfterDelay(float delay)
    {
        // Wait for the specified delay (10 seconds)
        yield return new WaitForSeconds(delay);

        // Render the third sprite
        secondSprite.SetActive(false);
        thirdSprite.SetActive(true);

        // Wait for the third sprite to become active
        yield return new WaitUntil(() => thirdSprite.activeInHierarchy);
    }

    IEnumerator ShowFourthSpriteAfterDelay(float delay)
    {
        // Wait for the specified delay (15 seconds)
        yield return new WaitForSeconds(delay);

        // Render the fourth sprite
        thirdSprite.SetActive(false);
        fourthSprite.SetActive(true);

        // Wait for the fourth sprite to become active
        yield return new WaitUntil(() => fourthSprite.activeInHierarchy);
    }

    IEnumerator ShowFifthSpriteAfterDelay(float delay)
    {
        // Wait for the specified delay (20 seconds)
        yield return new WaitForSeconds(delay);

        // Render the fifth sprite
        fourthSprite.SetActive(false);
        fifthSprite.SetActive(true);

        // Wait for the fifth sprite to become active
        yield return new WaitUntil(() => fifthSprite.activeInHierarchy);

        // Wait for another 5 seconds
        yield return new WaitForSeconds(5f);

        // Load the next scene
        SceneManager.LoadScene(2);
    }
}
