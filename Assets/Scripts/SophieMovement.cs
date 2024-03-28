using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float jumpPower;
    public float moveDirection;
    private bool facingRight = true;
    private bool isJumping = false;
    private bool hasJumped = false;
    private Animator animator;
    public LayerMask groundLayer;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();
        TryChangeScene();
    }

    void FixedUpdate()
    {
        Move();
    }

    public Transform boxCheck; // Assign this to a point near the player in the Inspector
    public float boxCheckRadius = 1.0f; // Adjust the radius as needed for your game
    public LayerMask boxLayer; // Assign a layer to your box and set it here in the Inspector

    private bool IsPlayerNearBox()
    {

        Debug.Log(boxCheck.position);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boxCheck.position, boxCheckRadius, boxLayer);
        Debug.Log(colliders);
        if (colliders.Length > 0)
        {
            Debug.Log("tes11");
            return true; // Player is near the box
        }
        Debug.Log("tesr");
        return false; // Player is not near the box
    }


    private void TryChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearBox())
        {
            Debug.Log("Changing Scene");
            SceneManager.LoadScene("SlidingTilePuzzle");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (boxCheck == null)
            return;

        Gizmos.DrawWireSphere(boxCheck.position, boxCheckRadius);
    }



    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !hasJumped)
        {
            isJumping = true;
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            hasJumped = true;
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
        //animator.SetBool("isJumping", isJumping);
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
