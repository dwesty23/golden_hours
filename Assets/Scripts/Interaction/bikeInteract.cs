using UnityEngine;

public class BikeInteract : Interactable
{

   public GameObject Sauravi;

    public override void Interact()
    {
        Puzzle3Manager.variable1 = true;
        Puzzle3Manager.Check();
        SauraviAppear();
        Debug.Log("Interacting with the bike");
       
        
    }

    public void SauraviAppear()
    {
        Debug.Log("Sauravi appears");
        Sauravi.SetActive(true);
        Invoke("DisableGameObjects", 3f);
        
    }

    private void DisableGameObjects()
    {
        Sauravi.SetActive(false);
    }



}