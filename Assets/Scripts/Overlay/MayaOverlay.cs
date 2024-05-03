using UnityEngine;
using UnityEngine.SceneManagement;

public class MayaOverlay : MonoBehaviour
{
    public void Start()
    {
        // Trigger Memory 3 scene after 5 seconds
        Invoke("TriggerMem3", 5);
    }
    public void TriggerMem3()
    {
        SceneManager.LoadScene("memory3");
    }
}