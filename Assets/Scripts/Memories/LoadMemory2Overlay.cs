using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Memory2Overlay : MonoBehaviour
{
    public GameObject[] sprites;
    public Conversation[] conversations;
    private int currentSprite = 0;
    private Coroutine spriteCoroutine;
    public AudioManaging audioManager;
    public GameObject speakerBox;

    void Start()
    {
        // Start with only the first sprite active
        StartCoroutine(ShowSprite(0));

        // Start the coroutine to show the next sprite after the conversation ends
        spriteCoroutine = StartCoroutine(ShowNextSpriteAfterConversation());
    }

    void Update()
    {
        // If the space key is pressed, stop the current coroutine and show the next sprite
        // if (Input.GetKeyDown(KeyCode.Space) && DialogueManagerM.IsConversationFinished())
        // {   
        //     if (currentSprite < sprites.Length - 1 && spriteCoroutine != null)
        //     {
        //         StopCoroutine(spriteCoroutine);
        //         ShowNextSprite();
        //     }
        //     if (currentSprite == sprites.Length - 1)
        //     {
        //         StopCoroutine(spriteCoroutine);
        //         SceneManager.LoadScene("Main");
        //     }
        // }
    }

    private IEnumerator ShowSprite(int index)
    {
        speakerBox.SetActive(false);
        // Deactivate all sprites
        foreach (GameObject sprite in sprites)
        {
            sprite.SetActive(false);
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
        float duration = 3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            spriteRenderer.color = color;
            yield return null;
        }
        DialogueManagerOverlay.StartConversation(conversations[index]);
        if (currentSprite != 4)
        {
            speakerBox.SetActive(true);
        }
        // Activate the sprite at the specified index
        if (index < 0 || index >= sprites.Length)
        {
            Debug.LogWarning("Invalid sprite index: " + index);
            yield break;
        }
        currentSprite = index;
        // spriteCoroutine = StartCoroutine(ShowNextSpriteAfterConversation());
    }

    private void ShowNextSprite()
    {
        // Increment the current sprite index
        currentSprite++;
        //Debug.Log("Current Sprite: " + currentSprite);
        // Check if the current sprite index is within the array bounds
        if (currentSprite < sprites.Length)
        {
            // Show the next sprite
            StartCoroutine(ShowSprite(currentSprite));

            spriteCoroutine = StartCoroutine(ShowNextSpriteAfterConversationDelay(5f));

            if (currentSprite == 4)
            {
                StartCoroutine(PlaySoundAndProceed());
            }
        }
    }

    private IEnumerator PlaySoundAndProceed()
    {
        speakerBox.SetActive(false);
        audioManager.PlaySFX(audioManager.puzzle2Complete);
        yield return new WaitForSeconds(15f);

        Debug.Log("Unloading memory2_overlay");
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.SetActive(true);
        }
        SceneManager.UnloadSceneAsync("memory2_overlay");
    }

    private IEnumerator ShowNextSpriteAfterConversationDelay(float delay)
    {
        // Wait for the specified delay
        //Debug.Log("Waiting for delay: " + delay);
        yield return new WaitForSeconds(delay);

        //Debug.Log("Delay finished");
        // Show the next sprite
        StartCoroutine(ShowNextSpriteAfterConversation());
    }

    private IEnumerator ShowNextSpriteAfterConversation()
    {
        // Wait until the dialogue is not active
        while (!DialogueManagerOverlay.IsConversationFinished())
        {
            yield return null;
        }
        //Debug.Log("Conversation finished");

        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
        }

        ShowNextSprite();
    }
}
