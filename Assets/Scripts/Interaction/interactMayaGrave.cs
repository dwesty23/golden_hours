using UnityEngine;

public class MayaGraveInteract : Interactable
{
    [SerializeField] private SceneField _SceneOverlay;
    public override void Interact()
    {
        Debug.Log("Interacting with the maya grave");
        OverlayInteract.Overlay(_SceneOverlay);
    }
}