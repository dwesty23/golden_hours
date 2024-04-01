using cakeslice;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset() 
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private OutlineEffect outlineEffect;

    public abstract void Interact();

    private void Awake()
    {
        // Get the OutlineEffect component
        outlineEffect = GetComponent<OutlineEffect>();
        Debug.Log(outlineEffect);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sophie"))
        {
            Debug.Log("Sophie is near the object. Press E to interact.");
            Debug.Log(outlineEffect);
            if (outlineEffect != null)
            {
                outlineEffect.enabled = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sophie")
        {
            Debug.Log("Sophie is no longer near the object.");
            Debug.Log(outlineEffect);
            if (outlineEffect != null)
            {
                outlineEffect.enabled = false;
            }
        }
    }
}