using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeGameInteract : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interacting with the arcade game");
        TriggerMemoryScene();
    }

    private void TriggerMemoryScene() {
        SceneManager.LoadScene("memory2");
    }
}