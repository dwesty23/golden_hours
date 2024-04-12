using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DinerManagerInteract : Interactable
{
    public Conversation diner1convo;
    public Conversation diner2convo;
    private bool hasInteracted = false;
    private bool diner1Finished = false;

    public override void Interact()
    {
        if (diner1Finished && !hasInteracted)
        {
            Debug.Log("Interacting with the Diner Manager");
            TriggerDiner2Convo();
        }
    }

    private void Start()
    {
        TriggerDiner1Convo();
    }

    private void TriggerDiner2Convo() {
        hasInteracted = true;
        DialogueManagerM.StartConversation(diner2convo);
        StartCoroutine(LoadSceneAfterDialogue());
    }

    private void TriggerDiner1Convo() {
        DialogueManagerM.StartConversation(diner1convo);
        StartCoroutine(HasDiner1Finished());
    }

    private IEnumerator HasDiner1Finished()
    {
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }
        Debug.Log("Diner 1 finished");
        diner1Finished = true;
    }

    private IEnumerator LoadSceneAfterDialogue()
    {
        // Wait until the dialogue is not active
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }
        // Load the scene
        SceneManager.LoadScene("SlidingTilePuzzle");
    }
}