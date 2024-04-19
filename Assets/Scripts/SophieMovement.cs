using UnityEngine;
using UnityEngine.SceneManagement;

public class SophieMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float jumpPower;
    public float moveDirection;
    public bool facingRight = false;
    private bool isJumping = false;
    private bool hasJumped = false;
    public bool currentlyInteracting = false; // Flag used in the police scene to stop sophie from moving while interacting
    public Animator animator;
    public LayerMask groundLayer;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    // sound stuff
    AudioManaging audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManaging>();    
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        if (!currentlyInteracting)
        {   
            moveDirection = Input.GetAxis("Horizontal");
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))  && !isJumping && !hasJumped)
            {
                isJumping = true;
                audioManager.PlaySFX(audioManager.jump);
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                hasJumped = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckInteraction();
                Debug.Log("E key pressed");
            }
        }
    }

    private void Move()
    {
        playerRb.velocity = new Vector2(moveDirection * speed, playerRb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0 && isJumping) // Check if collided object is on the ground layer and the player is jumping
        {
            hasJumped = false;
            isJumping = false;
            audioManager.PlaySFX(audioManager.land);
        }
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
        animator.SetFloat("horizontalValue", Mathf.Abs(moveDirection));
        animator.SetBool("isJumping", isJumping);
    }

    public void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
