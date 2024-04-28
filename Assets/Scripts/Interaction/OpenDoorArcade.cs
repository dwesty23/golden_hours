using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorArcade : Interactable
{

    [SerializeField] private SceneField sceneToLoad;
    public override void Interact()
    {
        Debug.Log("Interacting with the door");
        TriggerArcadeScene();
    }

    private void TriggerArcadeScene() {

        Scenes.Instance.SwitchToCutscene(sceneToLoad);
        //SceneManager.LoadScene("ArcadeOpenCutScene");
    }
}