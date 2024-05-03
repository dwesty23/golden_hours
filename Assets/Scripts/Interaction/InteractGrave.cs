using UnityEngine;

public class InteractGrave : Interactable
{
    [SerializeField] private SceneField _SceneOverlay;

    public override void Interact()
    {
        Debug.Log("Interacting with Grave"); 
        OverlayInteract.Overlay(_SceneOverlay);   
    }
}