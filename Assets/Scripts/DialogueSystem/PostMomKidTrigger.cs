using UnityEngine;
using System.Collections;

public class PostMomKidTrigger : MonoBehaviour
{
    public Conversation conversation;
    private bool dialogueTriggered = false;

    public void Start()
    {
        CheckAndTriggerDialogue();
    }

    public void Update()
    {
        CheckAndTriggerDialogue();
        if (DialogueManagerM.IsConversationFinished())
        {
            PlayerPrefs.SetInt("DialogueOn", 0);
        }
    }

    private void CheckAndTriggerDialogue()
    {
        if (!dialogueTriggered && (PlayerPrefs.GetInt("MomDiner") == 1) && (PlayerPrefs.GetInt("KidDiner") == 1) && (PlayerPrefs.GetInt("DialogueOn") == 0) && (PlayerPrefs.GetInt("PostMomKidTrigger") == 0))
        {
            PlayerPrefs.SetInt("MomDiner", 0);
            PlayerPrefs.SetInt("KidDiner", 0);
            PlayerPrefs.SetInt("DialogueOn", 1);
            StartCoroutine(DelayedTriggerDialogue());
            dialogueTriggered = true;
            PlayerPrefs.SetInt("PostMomKidTrigger", 1);
        }
    }

    IEnumerator DelayedTriggerDialogue()
    {
        yield return new WaitForSeconds(3f);
        DialogueManagerM.StartConversation(conversation);
    }
}