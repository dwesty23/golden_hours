using UnityEngine;
using UnityEngine.Audio;
public class maya_interaction : MonoBehaviour
{
    public Transform mayaTransform; // Assign Maya's transform in the Inspector
    public AudioSource mayaCries; // Assign an AudioSource with Maya's cries sound effect
    public float maxCryVolume = 1f; // Maximum volume when Sophie is right next to Maya
    public float minCryVolume = 0.1f; // Minimum volume when Sophie is far away from Maya
    public float maxCryDistance = 10f; // Distance at which Maya's cries are at minimum volume
    public GameObject dialogueManagerObject; // Assign this in the Inspector
    public Transform boxCheck; // Assign this to a point near the player in the Inspector
    public float boxCheckRadius = 1.0f; // Adjust the radius as needed for your game
    public LayerMask boxLayer; // Assign a layer to your box and set it here in the Inspector

    private DialogueManagerMM dialogueManager; // Reference to the DialogueManagerMM script
    void Start()
    {
        dialogueManager = dialogueManagerObject.GetComponent<DialogueManagerMM>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, mayaTransform.position);
        AdjustCryVolume(distance);

        TriggerDialogue();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCheck.position, boxCheckRadius);
    }

    private bool IsPlayerNearMaya()
    {   
        Debug.Log("BoxCheck position: " + boxCheck.position);
        Debug.Log("BoxCheck radius: " + boxCheckRadius);
        for (int i = 0; i < 32; i++)
        {
            if (((1 << i) & boxLayer) != 0)
            {
                Debug.Log("Layer in BoxLayer: " + LayerMask.LayerToName(i));
            }
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boxCheck.position, boxCheckRadius, boxLayer);
        Debug.Log("Number of colliders found: " + colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Collider found: " + collider.gameObject.name);
        }

        if (colliders.Length > 0)
        {
            Debug.Log("Player is near Maya");
            return true; // Player is near Maya
        }
        else{
            Debug.Log("Player is not near Maya");
            return false; // Player is not near Maya
        }
    }
    private void TriggerDialogue()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearMaya())
        {
            Debug.Log("Starting Dialogue");
            mayaCries.Stop(); // Stop the crying audio
            dialogueManager.Start();
        }
    }

    void AdjustCryVolume(float distance)
    {
        float volume = Mathf.Lerp(maxCryVolume, minCryVolume, distance / maxCryDistance);
        mayaCries.volume = Mathf.Clamp(volume, minCryVolume, maxCryVolume);
    }
}
