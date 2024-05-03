using UnityEngine;

public class OverlayInteract : MonoBehaviour 
{

    public static void Overlay( SceneField _SceneOverlay)
    {
        Scenes.Instance.SwitchToCutscene(_SceneOverlay);
    }
}
