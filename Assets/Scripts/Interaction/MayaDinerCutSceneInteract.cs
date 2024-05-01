using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MayaDinerCutSceneInteract : Interactable
{
    public Conversation convo;
    private bool hasInteracted = false;

    public override void Interact()
    {
        if (hasInteracted) return;
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        Debug.Log("Starting Dialogue");
        hasInteracted = true;
        DialogueManagerM.StartConversation(convo);
        //Load next scene after dialogue
        StartCoroutine(LoadSceneAfterDialogue());
    }

    private IEnumerator LoadSceneAfterDialogue()
    {
        // Wait until the dialogue is not active
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }

        // Load the scene as a coroutine
        Scenes.Instance.CompletePuzzle(1);
        PlayerPrefs.SetInt("DinerTrigger", 1);
        StartCoroutine(Scenes.Instance.LoadMap(true));
        
        // Scenes.Instance.LoadMap();
    }
}