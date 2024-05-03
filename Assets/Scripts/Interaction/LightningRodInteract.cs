using UnityEngine;

public class LightningRodInteract : Interactable
{

    public GameObject George;
    public Conversation convo;

    public override void Interact()
    {
        Puzzle3Manager.variable3 = true;
        Puzzle3Manager.Check();
        GeorgeAppear();
        Debug.Log("Interacting with lightning rod");
         
    }

    public void GeorgeAppear()
    {
        George.SetActive(true);
        Invoke("DisableGameObjects", 5f);
        DialogueManagerM.StartConversation(convo);
        
    }

    private void DisableGameObjects()
    {
        George.SetActive(false);
    }



}