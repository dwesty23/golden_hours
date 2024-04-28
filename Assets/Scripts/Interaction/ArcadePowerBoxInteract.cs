using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadePowerBoxInteract : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interacting with the power box");
        TriggerArcadeScene();
    }

    private void TriggerArcadeScene() {
        Debug.Log("Loading puzzle2 scene");
        SceneManager.LoadScene("puzzle2");
    }
}