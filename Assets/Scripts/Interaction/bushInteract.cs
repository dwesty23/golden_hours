using UnityEngine;

public class BushInteract : Interactable
{
    public GameObject bush2;
    public GameObject grave;

    public override void Interact()
    {
        
       Debug.Log("Interacting with the bush");
        DisableGameObjects();
        
    }

    private void DisableGameObjects()
    {
        bush2.SetActive(true);
        grave.SetActive(true);
        gameObject.SetActive(false);
    }



}