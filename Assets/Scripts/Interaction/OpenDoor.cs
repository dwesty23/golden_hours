using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : Interactable
{

    [SerializeField] private SceneField _sceneToLoad;
    public override void Interact()
    {
        Debug.Log("Interacting with the door");
        TriggerDinerScene();
    }

    private void TriggerDinerScene() {
        Scenes.Instance.SwitchToCutscene(_sceneToLoad);
        //SceneManager.LoadScene("DinerOpenCutScene");

    }
}