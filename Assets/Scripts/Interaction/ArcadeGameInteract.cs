using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ArcadeGameInteract : Interactable
{
    public Conversation arcade1convo;
    public Conversation arcade2convo;
    private bool hasInteracted = false;
    private bool arcade1Finished = false;

    public override void Interact()
    {
        if (arcade1Finished && !hasInteracted)
        {
            Debug.Log("Interacting with the arcade game");
            TriggerArcade2Convo();
        }
    }

    private void Start()
    {
        TriggerArcade1Convo();
    }

    private void TriggerArcade2Convo() {
        hasInteracted = true;
        DialogueManagerM.StartConversation(arcade2convo);
        StartCoroutine(TriggerMemoryScene());
    }

    private void TriggerArcade1Convo() {
        DialogueManagerM.StartConversation(arcade1convo);
        StartCoroutine(HasArcade1Finished());
    }

    private IEnumerator HasArcade1Finished()
    {
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }
        Debug.Log("Arcade 1 finished");
        arcade1Finished = true;
    }

    private IEnumerator TriggerMemoryScene() 
    {
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }
        SceneManager.LoadScene("memory2");
    }
}