using UnityEngine;

public class KidDinerInteract : Interactable
{
    public Conversation convo;
    private bool hasInteracted = false;

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
        if (DialogueManagerM.IsConversationFinished() && hasInteracted)
        {
            PlayerPrefs.SetInt("DialogueOn", 0);
            PlayerPrefs.SetInt("KidDiner", 1);
        }
    }
}