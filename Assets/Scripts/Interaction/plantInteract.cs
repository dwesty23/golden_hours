using UnityEngine;

public class PlantInteract : Interactable
{

    
    public GameObject plant;

    public override void Interact()
    {
        Debug.Log("Interacting with the plant");
        DisableGameObjects();
       
    }

    private void DisableGameObjects()
    {
        plant.SetActive(true);
        gameObject.SetActive(false);
    }


}