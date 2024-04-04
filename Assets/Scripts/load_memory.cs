using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpriteDisplayController : MonoBehaviour
{
    public GameObject[] sprites;
    private int currentSprite = 0;
    private Coroutine spriteCoroutine;

    void Start()
    {
        // Start with only the first sprite active
        StartCoroutine(ShowSprite(0));

        // Start the coroutine to show the next sprite
        spriteCoroutine = StartCoroutine(ShowNextSpriteAfterDelay(5f));
    }

    void Update()
    {
        // If the space key is pressed, stop the current coroutine and show the next sprite
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            if (currentSprite < sprites.Length - 1 && spriteCoroutine != null)
            {
                StopCoroutine(spriteCoroutine);
                ShowNextSprite();
            }
            if (currentSprite == sprites.Length - 1)
            {
                StopCoroutine(spriteCoroutine);
                SceneManager.LoadScene("Main");
            }
        }
    }

    private IEnumerator ShowSprite(int index)
    {
        // Deactivate all sprites
        foreach (GameObject sprite in sprites)
        {
            sprite.SetActive(false);
        }
        if (index == sprites.Length - 1)
        {
            SceneManager.LoadScene("Main");
        }
         // Activate the selected sprite
        GameObject selectedSprite = sprites[index];
        selectedSprite.SetActive(true);

        // Get the sprite renderer
        SpriteRenderer spriteRenderer = selectedSprite.GetComponent<SpriteRenderer>();

        // Set the initial color with 0 alpha
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        // Fade in over 1 second
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            spriteRenderer.color = color;
            yield return null;
        }
        // Activate the sprite at the specified index
        if (index < 0 || index >= sprites.Length)
        {
            Debug.LogWarning("Invalid sprite index: " + index);
            yield break;
        }
        currentSprite = index;
    }

    private void ShowNextSprite()
    {
        // Show the next sprite
        StartCoroutine(ShowSprite(currentSprite + 1));

        // Start the coroutine to show the next sprite after a delay
        spriteCoroutine = StartCoroutine(ShowNextSpriteAfterDelay(5f));
    }

    private IEnumerator ShowNextSpriteAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Show the next sprite
        ShowNextSprite();
    }
}
