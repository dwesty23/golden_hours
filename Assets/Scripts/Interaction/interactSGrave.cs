using UnityEngine;

public class interactSGrave : Interactable
{

    public GameObject sGrave;

    public override void Interact()
    {
      
        Debug.Log("Interacting with S Grave");
        DisableGameObjects();
         
    }

    private void DisableGameObjects()
    {
        sGrave.SetActive(true);
        gameObject.SetActive(false);
       
        Invoke("DisableGameObjects2", 4f);
    }

    private void DisableGameObjects2()
    {
        sGrave.SetActive(false);
    }

}