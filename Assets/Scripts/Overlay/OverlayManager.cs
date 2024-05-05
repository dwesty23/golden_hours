using UnityEngine;

public class OverlayManager : MonoBehaviour 
{
    void Update()
    {
        #if UNITY_WEBGL
            if (Input.GetKeyDown(KeyCode.Q))
            {            
                StartCoroutine(Scenes.Instance.LoadMap(true));
            }
        #else
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(Scenes.Instance.LoadMap(true));
            }
        #endif
    }
}