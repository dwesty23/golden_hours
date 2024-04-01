using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI to display dialogues
    private Queue<string> dialogues = new Queue<string>(); // Queue to store dialogues
    private bool isCurrentlyTyping = false; // Flag to check if currently typing
    private string currentDialogue = ""; // Store the current dialogue
    private string currentSpeaker = ""; // Store the current speaker's name

    public float typingSpeed = 0.05f; // Speed of typing effect

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
                dialogueText.text = currentSpeaker + currentDialogue; // Display the full dialogue instantly
                isCurrentlyTyping = false; // Update flag
            }
            else if (!isCurrentlyTyping && dialogues.Count > 0)
            {
                DisplayNextDialogue(); // Display next dialogue
            }
            else
            {
                EndDialogue();
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
        isCurrentlyTyping = true; // Update flag
        dialogueText.text = currentSpeaker; // Start with the speaker's name
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter; // Add each letter one by one
            yield return new WaitForSeconds(typingSpeed); // Wait between each letter
        }
        
        isCurrentlyTyping = false; // Update flag
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
            currentSpeaker = fullDialogue.Substring(0, colonIndex + 1) + " "; // Include the colon and space
            currentDialogue = fullDialogue.Substring(colonIndex + 2); // Skip the ": " to get the dialogue
        }
        else // If no colon found, treat the whole string as dialogue
        {
            currentSpeaker = "";
            currentDialogue = fullDialogue;
        }
    }
}


