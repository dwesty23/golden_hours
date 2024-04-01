using cakeslice;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    public Material defaultMaterial;
    public Material outlineMaterial;
    private SpriteRenderer spriteRenderer;
    private void Reset() 
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void Interact();

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.material = defaultMaterial;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sophie"))
        {
            Debug.Log("Sophie is near the object. Press E to interact.");
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer is null");
                return;
            }
            if (outlineMaterial == null)
            {
                Debug.LogError("OutlineMaterial is null");
                return;
            }
            spriteRenderer.material = outlineMaterial;     
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sophie")
        {
            Debug.Log("Sophie is no longer near the object.");
            spriteRenderer.material = defaultMaterial;
        }
    }
}