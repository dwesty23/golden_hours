using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ArcadePowerBoxInteract : Interactable
{
    public Conversation convo;
    public override void Interact()
    {
        Debug.Log("Interacting with the power box");
        TriggerArcadeScene();
    }

    private void TriggerArcadeScene() {
        //Trigger Dialogue
        PlayerPrefs.SetInt("DialogueOn", 1);
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
        PlayerPrefs.SetInt("DialogueOn", 0);
        SceneManager.LoadScene("puzzle2");
    }
}