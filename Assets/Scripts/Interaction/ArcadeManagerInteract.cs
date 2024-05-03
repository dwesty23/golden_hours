using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ArcadeManagerInteract : Interactable
{
    public Conversation convo;
    public Conversation convo2;
    private bool hasInteracted = false;
    private bool hasInteracted2 = false;

    public override void Interact()
    {
        // Check if a dialgue is curently active 
        if (PlayerPrefs.GetInt("DialogueOn") == 1 || hasInteracted) return;
        PlayerPrefs.SetInt("DialogueOn", 1);
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        Debug.Log("Starting Dialogue");
        hasInteracted = true;
        DialogueManagerM.StartConversation(convo);
    }

    private void Update()
    {
        // Check if the dialogue is finished
        if (DialogueManagerM.IsConversationFinished() && hasInteracted && !hasInteracted2)
        {
            PlayerPrefs.SetInt("DialogueOn", 0);
            StartCoroutine(TriggerSecondDialogue());
            hasInteracted2 = true;
        }
    }

    IEnumerator TriggerSecondDialogue()
    {
        yield return new WaitForSeconds(3f);
        DialogueManagerM.StartConversation(convo2);
    }
}