using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GeorgeInteract : Interactable
{
    public GameObject character1;
    public GameObject character2;
    public Conversation convo;
    public Conversation convo2;

    public override void Interact()
    {
        Debug.Log("Interacting with George");
        if (PlayerPrefs.GetInt("DialogueOn") == 1 || PlayerPrefs.GetInt("GeorgeInteract") == 1) return;
        PlayerPrefs.SetInt("DialogueOn", 1);
        PlayerPrefs.SetInt("GeorgeInteract", 1);
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        DialogueManagerM.StartConversation(convo);
        StartCoroutine(DisableGameObjects());
    }

    private IEnumerator DisableGameObjects()
    {
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }

        Scenes.Instance.CompletePuzzle(3);
        PlayerPrefs.SetInt("DialogueOn", 0);
        StartCoroutine(TriggerSecondDialogue());
    }

    private IEnumerator TriggerSecondDialogue()
    {
        yield return new WaitForSeconds(3f);
        character1.SetActive(false);
        character2.SetActive(false);
        gameObject.SetActive(false);
        DialogueManagerM.StartConversation(convo2);
        PlayerPrefs.SetInt("DialogueOn", 1);
    }
}