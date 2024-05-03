using UnityEngine;

public class OverlayManager : MonoBehaviour 
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            StartCoroutine(Scenes.Instance.LoadMap(true));
        }
    }
}