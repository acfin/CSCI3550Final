using EasyParallax;
using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // public variables appear as properties in Unity's inspector window
    public Rigidbody2D rb;
    public float jumpForce = 10;
    public float jumpGravity = 10;
    public float fallingGravity = 40;
    public float movementSpeed = 3.0f;
    
    // holds 2D points; used to represent a character's location in 2D space, or where it's moving to
    //private float movement;
    private Vector2 movement = new Vector2();

    // holds reference to the animator component in the game object
    Animator animator;

    AudioSource audio;

    // reference to the character's Rigidbody2D component
    Rigidbody2D rb2D;
    private CapsuleCollider2D bc2D;
    Coroutine damageCoroutine;

    // use this for initialization
    private void Start()
    {
        // get references to game object components so they don't have to be grabbed each time they are needed
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();

    }

    // called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audio.Play();
        }

        // update the animation state machine
        UpdateState();
    }

    // called at fixed intervals by the Unity engine
    // update may be called less frequently on slower hardware when frame rate slows down
    void FixedUpdate()
    {


        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = jumpGravity;
        }
        
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravity;
        }
        
        MoveCharacter();
    }

    private void UpdateState()
    {
        // Check to see if the movement vector is approximately equal to (0, 0) -- i.e. player is standing still
        if (Mathf.Approximately(movement.x, 0))
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        // Update the animator with the new movement values
        animator.SetFloat("xDir", movement.x);
    }
    

    private bool CanJump()
    {
        if (bc2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveCharacter()
    {
        // get user input
        // GetAxisRaw parameter allows us to specify which axis we're interested in
        // Returns 1 = right key or "d" (up key or "w")
        //        -1 = left key or "a"  (down key or "s")
        //         0 = no key pressed
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        
        // keeps player moving at the same rate of speed, no matter which direction they are moving in
        //movement.Normalize();

        // set velocity of RigidBody2D and move it
        rb2D.velocity = new Vector2(movement.x * movementSpeed, rb2D.velocity.y);
        //SetBackgroundSpeed(movement.x);
    }
}