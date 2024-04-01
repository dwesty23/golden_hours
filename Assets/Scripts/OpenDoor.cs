using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interacting with the door");
        TriggerDinerScene();
    }

    private void TriggerDinerScene() {
        SceneManager.LoadScene("SlidingTilePuzzle");
    }
}