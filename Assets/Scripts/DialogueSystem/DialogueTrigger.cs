using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Conversation conversation;
    public string dialogueTriggerName;

    public void Start()
    {   
        if (PlayerPrefs.GetInt(dialogueTriggerName) == 0)
        {
            TriggerDialogue();
            PlayerPrefs.SetInt(dialogueTriggerName, 1);
        }
    }
    public void TriggerDialogue()
    {
        DialogueManagerM.StartConversation(conversation);
    }
}