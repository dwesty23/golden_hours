using UnityEngine;

public class MomDinerInteract : Interactable
{
    public Conversation convo;
    private bool hasInteracted = false;

    public override void Interact()
    {
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
        if (DialogueManagerM.IsConversationFinished() && hasInteracted)
        {
            PlayerPrefs.SetInt("DialogueOn", 0);
            PlayerPrefs.SetInt("MomDiner", 1);
        }
    }
}