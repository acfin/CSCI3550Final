using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpSpeed = 5;
    Animator animator;
    bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Debug.Log(animator.GetBool("IsMoving"));
    }

    private void Movement()
    {
        //If we are moving right
        if (Input.GetAxis("Horizontal") > 0)
        {
            player.transform.position = new Vector3(player.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, player.transform.position.y);
            animator.SetBool("IsMoving", true);
            //If we are not currently facing right, rotate the player and face right
            if (!facingRight)
            {
                playerRenderer.flipX = false;
                facingRight = true;
            }
        } // If we are moving left
        else if(Input.GetAxis("Horizontal") < 0)
        {
            player.transform.position = new Vector3(player.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, player.transform.position.y);
            animator.SetBool("IsMoving", true);
            //If we are not currently facing left, rotate and face left
            if (facingRight)
            {
                playerRenderer.flipX = true;
                facingRight = false;
            }
        } // If we are not moving
        else
        {
            animator.SetBool("IsMoving", false);
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(new Vector2(0, jumpSpeed * 100));
        }
    }
}
