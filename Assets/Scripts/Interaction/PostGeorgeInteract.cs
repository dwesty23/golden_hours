using UnityEngine;

public class PostGeorgeInteract : Interactable
{
    public Conversation convo;
    private bool hasInteracted = false;
    public override void Interact()
    {
        Debug.Log("Interacting with George");
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
}