using UnityEngine;

public class MayaInteract : Interactable
{
    public Transform SophieTransform; // Assign Sophie's transform in the Inspector
    public AudioSource mayaCries; // Assign an AudioSource with Maya's cries sound effect
    public float maxCryVolume = 1f; // Maximum volume when Sophie is right next to Maya
    public float minCryVolume = 0.1f; // Minimum volume when Sophie is far away from Maya
    public float maxCryDistance = 10f; // Distance at which Maya's cries are at minimum volume
    public GameObject dialogueManagerObject; // Assign this in the Inspector
    private DialogueManagerMM dialogueManager; // Reference to the DialogueManagerMM script

    public override void Interact()
    {
        Debug.Log("Interacting with Maya");
        TriggerDialogue();
    }

    void Start()
    {
        dialogueManager = dialogueManagerObject.GetComponent<DialogueManagerMM>();
    }

    void Update()
    {
        float distance = Vector3.Distance(SophieTransform.position, transform.position);
        AdjustCryVolume(distance);
    }
    private void TriggerDialogue()
    {
        Debug.Log("Starting Dialogue");
        mayaCries.Stop(); // Stop the crying audio
        dialogueManager.Start();
    }

    void AdjustCryVolume(float distance)
    {
        float volume = Mathf.Lerp(minCryVolume, maxCryVolume, distance / maxCryDistance); 
        mayaCries.volume = Mathf.Clamp(volume, minCryVolume, maxCryVolume);
    }
}