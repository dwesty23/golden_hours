using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MayaInteract : Interactable
{
    public Transform SophieTransform; // Assign Sophie's transform in the Inspector
    public AudioSource mayaCries; // Assign an AudioSource with Maya's cries sound effect
    public float maxCryVolume = 1f; // Maximum volume when Sophie is right next to Maya
    public float minCryVolume = 0.1f; // Minimum volume when Sophie is far away from Maya
    public float maxCryDistance = 10f; // Distance at which Maya's cries are at minimum volume
    public Conversation convo;
    private bool hasInteracted = false;
    public override void Interact()
    {
        Debug.Log("Interacting with Maya");
        if (hasInteracted) return;
        TriggerDialogue();
    }

    void Update()
    {
        float distance = Vector3.Distance(SophieTransform.position, transform.position);
        AdjustCryVolume(distance);
    }
    private void TriggerDialogue()
    {
        Debug.Log("Starting Dialogue");
        hasInteracted = true;
        mayaCries.Stop(); // Stop the crying audio
        DialogueManagerM.StartConversation(convo);
        //Load next scene after dialoguee
        StartCoroutine(LoadSceneAfterDialogue());
    }

    private IEnumerator LoadSceneAfterDialogue()
    {
        // Wait until the dialogue is not active
        while (!DialogueManagerM.IsConversationFinished())
        {
            yield return null;
        }

        // Load the scene
        PlayerPrefs.SetInt("DinerTrigger", 0);
        PlayerPrefs.SetInt("PostArcadeTrigger", 1);
        PlayerPrefs.SetInt("PostMem3Trigger", 1);
        StartCoroutine(Scenes.Instance.LoadMap());
    }

    void AdjustCryVolume(float distance)
    {
        float volume = Mathf.Lerp(minCryVolume, maxCryVolume, distance / maxCryDistance); 
        mayaCries.volume = Mathf.Clamp(volume, minCryVolume, maxCryVolume);
    }
}