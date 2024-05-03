using UnityEngine;

public class GeorgeInteract : Interactable
{
    public GameObject character1;
    public GameObject character2;

    public override void Interact()
    {
        Debug.Log("Interacting with George");
        DisableGameObjects();
        Scenes.Instance.CompletePuzzle(3);
    }

    private void DisableGameObjects()
    {
        character1.SetActive(false);
        character2.SetActive(false);
        gameObject.SetActive(false);
    }
}