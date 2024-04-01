using cakeslice;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset() 
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private Outline outline;

    public abstract void Interact();

    private void Start()
    {
        // Get the Outline component
        outline = GetComponent<Outline>();
    }
    private void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (outline != null)
            {
                outline.enabled = true;
            }
        }
    }
    
    private void onTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }
}