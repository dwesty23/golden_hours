using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI to display dialogues
    public Image characterImage; // Image to display character portraits of whose speaking
    public Sprite sophieSpeakingSprite, sophieNotSpeakingSprite; // Sprites for Sophie
    public Sprite officerSpeakingSprite, officerNotSpeakingSprite; // Sprites for Officer
    public TMP_FontAsset sophieFont; // Font for Sophie's dialogues
    public TMP_FontAsset officerFont; // Font for Officer's dialogues
    private Queue<string> dialogues = new Queue<string>(); // Queue to store dialogues
    private bool isCurrentlyTyping = false; // Flag to check if currently typing
    private bool animationOver = false; // Flag to check if animation is over
    private bool finalSpace = false; // Flag to check if the final space key has been pressed
    private string currentDialogue = ""; // Store the current dialogue
    private string currentSpeaker = ""; // Store the current speaker's name
    public GameObject textBubble; // Reference to the text bubble game object
    public GameObject journalIcon; // Reference to the journal game object

    [Header("Scene to Load")]
    [SerializeField] private SceneField _levelSceneMeetingMaya;
    [SerializeField] private SceneField _JournalControls;

    [Header("Sophie Movement")]
    public SophieMovement sophieMovement; // Reference to the SophieMovement script

    void Start()
    {
        characterImage.enabled = false; // Hide the character image initially
        journalIcon.SetActive(false); // Hide the journal icon initially
        textBubble.SetActive(false); // Hide the text bubble initially
        // Enqueue the dialogues
        dialogues.Enqueue("Sophie: Excuse me? Excuse me!");

        dialogues.Enqueue("Officer: Hm?");

        dialogues.Enqueue("Sophie: I’ve been here for an hour and nobody has helped me. Can I PLEASE talk to you?");

        dialogues.Enqueue("Officer: Ma’am, you have to wait your turn, I’m sure everybody here needs to be helped just as much as you do.");

        dialogues.Enqueue("Sophie: My little sister Amber is missing, she’s 10.");

        dialogues.Enqueue("Sophie: Last night, we got in an… argument. She stormed off and I haven’t seen her since.");

        dialogues.Enqueue("Sophie: I already called her friends, they haven’t seen her either. Please, help-");

        dialogues.Enqueue("Officer: Wait. In. Line.");

        dialogues.Enqueue("Sophie: But I-");
        
        dialogues.Enqueue("Officer: In LINE ma’am. Or I will have to escort you out of the premises.");

        dialogues.Enqueue("Sophie: Forget it. I’ll find her myself."); 

        //DisplayNextDialogue();
        StartCoroutine(MoveSophie());
    }

    private IEnumerator MoveSophie()
    {
        sophieMovement.currentlyInteracting = true;  // Set the currentlyInteracting flag to true to stop Sophie from moving by player input
        sophieMovement.FlipCharacter();  // Ensure Sophie is facing right
        UpdateCharacterImage(false);  // Ensure the character image updates to not speaking

        float duration = 2.7f;  // Duration Sophie should keep moving
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            sophieMovement.moveDirection = 1;  // Keep setting moveDirection to 1 to ensure continuous movement
            sophieMovement.animator.SetFloat("horizontalValue", 1);  // Update animation speed or state
            yield return null;  // Wait until the next frame
        }

        // Transition to idle animation
        sophieMovement.moveDirection = 0;  // Stop moving Sophie by setting moveDirection to 0
        sophieMovement.animator.SetFloat("horizontalValue", 0);  // Reset the walking animation speed or state to idle

        // Ensure the animation resets correctly
        yield return new WaitForSeconds(0.5f);  // Short delay to allow the animation to transition back to idle smoothly

        textBubble.SetActive(true); // Show the text bubble
        animationOver = true;  // Update the animationOver flag
        characterImage.enabled = true;  // Show the character image
        Invoke("DisplayNextDialogue", 0.2f);  // Proceed to the next dialogue after a short delay
        //DisplayNextDialogue();  // Proceed to the next dialogue
    }

    void Update()
    {
        if (animationOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!finalSpace)
                {
                    if (isCurrentlyTyping)
                    {
                        StopAllCoroutines(); // Stop the typing coroutine
                        dialogueText.text = currentDialogue; // Display the full dialogue instantly
                        isCurrentlyTyping = false; // Update flag
                        UpdateCharacterImage(false); // Ensure the character image updates to not speaking
                    }
                    else if (!isCurrentlyTyping && dialogues.Count > 0)
                    {
                        DisplayNextDialogue(); // Proceed to display the next dialogue
                    }
                    else if (!isCurrentlyTyping && dialogues.Count == 0)
                    {
                        // Clear the dialogue text and set the text bubble to inactive
                        dialogueText.text = "";
                        textBubble.SetActive(false);
                        finalSpace = true; // Update the finalSpace flag
                        SceneManager.LoadSceneAsync(_JournalControls, LoadSceneMode.Additive);
                        sophieMovement.currentlyInteracting = false; // Update the currentlyInteracting flag
                        journalIcon.SetActive(true); // Show the journal icon
                    }
                }
            }
            else if (!isCurrentlyTyping && dialogues.Count == 0)
            {
                if (sophieMovement.transform.position.x < -9.2)
                {
                    // Start the main scene once Sophie has moved off-screen to the left
                    StartMain();
                }
                else if (sophieMovement.transform.position.x > 7.5)
                {
                    // Restrict movement to the right by setting moveDirection based on the current player input
                    float moveHorizontal = Input.GetAxis("Horizontal");

                    // Only allow leftward movement (negative direction)
                    if (moveHorizontal < 0)
                    {
                        sophieMovement.moveDirection = moveHorizontal;
                        sophieMovement.animator.SetFloat("horizontalValue", Mathf.Abs(moveHorizontal));
                    }
                    else
                    {
                        // Stop any rightward or stationary movement
                        sophieMovement.moveDirection = 0;
                        sophieMovement.animator.SetFloat("horizontalValue", 0);
                    }
                }
            }

        }
    }

    public void DisplayNextDialogue()
    {

        if (dialogues.Count == 0)
        {
            textBubble.SetActive(false); // Hide the text bubble if there are no more dialogues
            return; // Exit the method early
        }

        string fullDialogue = dialogues.Dequeue();
        SeparateDialogue(fullDialogue);
        StartCoroutine(TypeDialogue(currentDialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        UpdateCharacterImage(true); // Set to speaking state
        isCurrentlyTyping = true;
        dialogueText.text = ""; // Append a space after the speaker's name for readability

        int i = 0;
        // Start iterating from the point after the speaker's name to skip typing it out again
        while (i < dialogue.Length)
        {
            if (dialogue[i] == '<')
            {
                int tagClose = dialogue.IndexOf('>', i);
                if (tagClose != -1)
                {
                    // Instantly append the entire tag to the text
                    dialogueText.text += dialogue.Substring(i, tagClose - i + 1);
                    i = tagClose; // Skip ahead to the end of the tag
                }
            }
            else
            {
                // Type out the dialogue character by character
                dialogueText.text += dialogue[i];
                yield return new WaitForSeconds(0.07f);
            }
            i++;
        }

        UpdateCharacterImage(false); // Set to not speaking state once done
        isCurrentlyTyping = false;
    }

    private void UpdateCharacterImage(bool isSpeaking)
    {
        // Default to not speaking sprites initially
        Sprite newSprite = sophieNotSpeakingSprite; // Default sprite

        if (currentSpeaker.ToLower().Contains("sophie"))
        {
            newSprite = isSpeaking ? sophieSpeakingSprite : sophieNotSpeakingSprite;
        }
        else if (currentSpeaker.ToLower().Contains("officer"))
        {
            newSprite = isSpeaking ? officerSpeakingSprite : officerNotSpeakingSprite;
        }

        characterImage.sprite = newSprite; // Update the sprite
    }

    public void StartMain()
    {
        SceneManager.LoadScene(_levelSceneMeetingMaya, LoadSceneMode.Single);
    }

    private void SeparateDialogue(string fullDialogue)
    {
        int colonIndex = fullDialogue.IndexOf(':');
        if (colonIndex != -1)
        {
            string speaker = fullDialogue.Substring(0, colonIndex).ToLower(); // Get the speaker in lowercase
            currentDialogue = fullDialogue.Substring(colonIndex + 2); // Extract dialogue part

            switch (speaker)
            {
                case "sophie":
                    // For Sophie: everything in lowercase, no extra styling
                    currentSpeaker = "Sophie:"; // Keeping the initial capital for names for readability
                    currentDialogue = currentDialogue.ToLower(); // Convert dialogue to lowercase
                    dialogueText.font = sophieFont; // Assuming you have a specific font for Sophie
                    break;
                case "officer":
                    // For the Officer: bold and uppercase
                    currentSpeaker = "<b>OFFICER:</b>"; // Make name bold and uppercase
                    currentDialogue = $"<b>{currentDialogue.ToUpper()}</b>"; // Convert dialogue to uppercase and make bold
                    dialogueText.font = officerFont; // Assuming you have a specific font for the Officer
                    break;
                default:
                    // Default case if needed
                    break;
            }
        }
        else // If no colon is found, treat the whole string as dialogue
        {
            currentSpeaker = ""; // No speaker name
            currentDialogue = fullDialogue; // Use the original string as is
        }
    }
}



