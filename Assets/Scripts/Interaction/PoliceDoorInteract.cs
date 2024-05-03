using UnityEngine;

public class PoliceDoorInteract : Interactable
{
    [SerializeField] private SceneField sceneToLoad;

    public override void Interact()
    {
        Debug.Log("Interacting with the door");
        TriggerEndScene();
    }

    private void TriggerEndScene()
    {
        Scenes.Instance.SwitchToCutscene(sceneToLoad);
    }
}