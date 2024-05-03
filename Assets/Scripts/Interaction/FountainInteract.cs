using UnityEngine;

public class FountainInteract : Interactable
{

    public GameObject david;
    public Conversation convo;

    public override void Interact()
    {
         Puzzle3Manager.variable2 = true;
         Puzzle3Manager.Check();
         DavidAppear();
         
        Debug.Log("Interacting with the fountain");
       
    }

    public void DavidAppear()
    {
        david.SetActive(true);
        Invoke("DisableGameObjects", 5f);
        DialogueManagerM.StartConversation(convo);
       
    }

    private void DisableGameObjects()
    {
        david.SetActive(false);
    }
}