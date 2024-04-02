using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI to display dialogues

    public TMP_FontAsset sophieFont; // Font for Sophie's dialogues
    public TMP_FontAsset officerFont; // Font for Officer's dialogues

    private Queue<string> dialogues = new Queue<string>(); // Queue to store dialogues
    private bool isCurrentlyTyping = false; // Flag to check if currently typing
    private string currentDialogue = ""; // Store the current dialogue
    private string currentSpeaker = ""; // Store the current speaker's name

    public float typingSpeed = 0.12f; // Speed of typing effect

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
                // Ensure the full dialogue is displayed with the proper spacing after the colon
                dialogueText.text = currentSpeaker + " " + currentDialogue; // Append the space correctly
                isCurrentlyTyping = false; // Update flag
            }
            else if (!isCurrentlyTyping && dialogues.Count > 0)
            {
                DisplayNextDialogue(); // Proceed to display the next dialogue
            }
            else
            {
                EndDialogue(); // Or handle the end of dialogues appropriately
            }
        }
    }

    public void DisplayNextDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentDialogue = dialogues.Dequeue();
        SeparateDialogue(currentDialogue);
        StopAllCoroutines(); // Stop current coroutine to ensure no overlap
        StartCoroutine(TypeDialogue(currentDialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isCurrentlyTyping = true;
        dialogueText.text = currentSpeaker + " "; // Set the speaker's name and colon here, adding space after colon

        foreach (char letter in dialogue.ToCharArray()) // Use the dialogue part without the speaker's name
        {
            dialogueText.text += letter; // Add each letter one by one
            yield return new WaitForSeconds(typingSpeed);
        }

        isCurrentlyTyping = false;
    }

    void EndDialogue()
    {
        SceneManager.LoadScene(2); // Load next scene after dialogue ends
    }

    private void SeparateDialogue(string fullDialogue)
    {
        int colonIndex = fullDialogue.IndexOf(':');
        if (colonIndex != -1)
        {
            // Extract the speaker's name from the dialogue string
            currentSpeaker = fullDialogue.Substring(0, colonIndex + 1); // Include the colon

            // Extract the dialogue part, keeping the name and colon for display
            currentDialogue = fullDialogue.Substring(colonIndex + 2); // Start after the colon and space

            // Change the font based on the speaker
            switch (currentSpeaker.ToLower().Trim(':'))
            {
                case "sophie":
                    dialogueText.font = sophieFont;
                    Console.WriteLine("Sophie's font");
                    break;
                case "officer":
                    dialogueText.font = officerFont;
                    Console.WriteLine("Officer's font");
                    break;
                default:
                    dialogueText.font = sophieFont; // Use a default font if no specific character is matched
                    break;
            }
        }
        else // No colon found, treat the whole string as dialogue
        {
            currentSpeaker = ""; // Clear the speaker
            currentDialogue = fullDialogue; // Use the original string
            dialogueText.font = sophieFont; // Use a default font
        }
    }
}


