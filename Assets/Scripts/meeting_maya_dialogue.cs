using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DialogueManagerMM : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI to display dialogues

    private Queue<string> dialogues; // Queue to store dialogues

    public float typingSpeed = 0.05f; // Speed of typing effect
    public float pauseAfterDialogue = 1f; // Pause after each dialogue

    public void Start()
    {
        dialogues = new Queue<string>();

        // Enqueue the dialogues
        dialogues.Enqueue("Sophie: Excuse me? Excuse me!");
        dialogues.Enqueue("Officer: Hm?");
        dialogues.Enqueue("Sophie: I’ve been here for an hour and nobody has helped me. Can I PLEASE talk to you?");
        dialogues.Enqueue("Officer: Ma’am, you have to wait your turn, I’m sure everybody here needs to be helped just as much as you do.");
        dialogues.Enqueue("Sophie: My little sister is missing, she’s 10. Last night, we got in a… fight. Just a small argument really. She stormed off and I haven’t seen her since last night. I called her friends, nobody has seen her. Please, help-");
        dialogues.Enqueue("Officer: Wait. In. Line.");
        dialogues.Enqueue("Sophie: But I-");
        dialogues.Enqueue("Officer: In LINE ma’am. Or I will have to escort you out of the premises.");
        dialogues.Enqueue("Sophie: Forget it. I’ll find her myself."); 

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        string dialogue = dialogues.Dequeue();
        StopAllCoroutines(); // Stop current coroutine to ensure no overlap
        StartCoroutine(TypeDialogue(dialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // Clear text at the start
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter; // Add each letter one by one
            yield return new WaitForSeconds(typingSpeed); // Wait between each letter
        }
        
        yield return new WaitForSeconds(pauseAfterDialogue); // Wait after the dialogue is complete before proceeding

        DisplayNextDialogue(); // Automatically proceed to next dialogue
    }

    void EndDialogue()
    {
        SceneManager.LoadScene(2); // Load next scene after dialogue ends
    }
}

