using UnityEngine;

public class interactGraveDavid : Interactable
{
    public GameObject grave;


    public override void Interact()
    {
      
        Debug.Log("Interacting with David Grave");
        DisableGameObjects();
         
    }

    private void DisableGameObjects()
    {
        grave.SetActive(true);
        gameObject.SetActive(false);
        // wait 4 seconds
        Invoke("DisableGameObjects2", 4f);
    }

    private void DisableGameObjects2()
    {
        grave.SetActive(false);
    }

    



}