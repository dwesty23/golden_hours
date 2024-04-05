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
    private string currentDialogue = ""; // Store the current dialogue
    private string currentSpeaker = ""; // Store the current speaker's name
    public float typingSpeed = 0.12f; // Speed of typing effect

    [Header("Scenes to Load")]  
    [SerializeField] private SceneField _persistentGameplay;
    [SerializeField] private SceneField _levelSceneMain;

    void Start()
    {
        // Enqueue the dialogues
        dialogues.Enqueue("Sophie: Excuse me? Excuse me!");
        dialogues.Enqueue("Officer: Hm?");
        dialogues.Enqueue("Sophie: I’ve been here for an hour and nobody has helped me. Can I PLEASE talk to you?");
        dialogues.Enqueue("Officer: Ma’am, you have to wait your turn.");
        dialogues.Enqueue("Officer: I’m sure everybody here needs to be helped just as much as you do.");
        dialogues.Enqueue("Sophie: My little sister is missing, she’s 10. Last night, we got in a… fight.");
        dialogues.Enqueue("Sophie: Just a small argument really. She stormed off and I haven’t seen her since last night.");
        dialogues.Enqueue("Sophie: I called her friends, nobody has seen her. Please, help-");
        dialogues.Enqueue("Officer: Wait. In. Line.");
        dialogues.Enqueue("Sophie: But I-");
        dialogues.Enqueue("Officer: In LINE ma’am. Or I will have to escort you out of the premises.");
        dialogues.Enqueue("Sophie: Forget it. I’ll find her myself."); 

        DisplayNextDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCurrentlyTyping)
            {
                StopAllCoroutines(); // Stop the typing coroutine
                dialogueText.text = currentSpeaker + " " + currentDialogue; // Display the full dialogue instantly
                isCurrentlyTyping = false; // Update flag
                UpdateCharacterImage(false); // Ensure the character image updates to not speaking
            }
            else if (!isCurrentlyTyping && dialogues.Count > 0)
            {
                DisplayNextDialogue(); // Proceed to display the next dialogue
            }
            else if (!isCurrentlyTyping && dialogues.Count == 0)
            {
                StartMain(); // Load the main scene
            }
        }
    }

    public void DisplayNextDialogue()
    {
        if (dialogues.Count == 0)
        {
            StartMain();
            return;
        }

        string fullDialogue = dialogues.Dequeue();
        SeparateDialogue(fullDialogue);
        StartCoroutine(TypeDialogue(currentDialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        UpdateCharacterImage(true); // Set to speaking state
        isCurrentlyTyping = true;
        dialogueText.text = currentSpeaker + " "; // Append a space after the speaker's name for readability

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
                yield return new WaitForSeconds(typingSpeed);
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
        SceneManager.LoadSceneAsync(_persistentGameplay);
        SceneManager.LoadSceneAsync(_levelSceneMain, LoadSceneMode.Additive);
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



