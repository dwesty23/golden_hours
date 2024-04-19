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

    [Header("Persistence scene")]
    [SerializeField] private SceneField _persistentScene;
    [Header("Scenes to Load")]
    [SerializeField] private SceneField[] scenesToLoad;
    
    [SerializeField] private SceneField _mainScene;

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
        SceneManager.LoadSceneAsync(_persistentScene);
        // iterate through scenes to load and load them additively
        // foreach (SceneField scene in scenesToLoad)
        // {
        //     SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        // }
        SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
    }

    void AdjustCryVolume(float distance)
    {
        float volume = Mathf.Lerp(minCryVolume, maxCryVolume, distance / maxCryDistance); 
        mayaCries.volume = Mathf.Clamp(volume, minCryVolume, maxCryVolume);
    }
}