using UnityEngine;

public class interactGraveGeorge : Interactable
{
    public GameObject GeorgeGrave;

    public override void Interact()
    {
      
        Debug.Log("Interacting with George Grave");
        
        
         
    }
    
    private void DisableGameObjects()
    {
        GeorgeGrave.SetActive(true);
        gameObject.SetActive(false);
        // wait 4 seconds
        Invoke("DisableGameObjects2", 4f);
    }

    private void DisableGameObjects2()
    {
        GeorgeGrave.SetActive(false);
    }

}