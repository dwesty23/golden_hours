using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D playerRb;
    public float speed;
    public float moveDirection;
    private bool facingRight = true;
    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if(moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if(moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    void FixedUpdate() 
    {
        playerRb.velocity = new Vector2(moveDirection * speed, playerRb.velocity.y);    
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
