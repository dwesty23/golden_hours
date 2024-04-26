using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorArcade : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interacting with the door");
        TriggerArcadeScene();
    }

    private void TriggerArcadeScene() {
        SceneManager.LoadScene("ArcadeOpenCutScene");
    }
}