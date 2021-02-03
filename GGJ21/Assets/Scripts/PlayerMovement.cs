using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.PlayerSounds;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private AnimationStateController animCtrl; //Calls changes to animations so that inputs for the player character are all contained in PlayerMovement (and Abilities)

    public float speed;
    public float jumpPower;
    public float gravity;
    public static int jumps = 1;
    public static bool active = true;
    private bool facingRight = true; //This will indicate the char is facing right

    public Animator animator;

    private float yVelocity;
    private int jumpsLeft = 0; //Number of midair jumps left (default = jumps - 1)

    public bool isWalking;

    public playerSounds playerSounds;



    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        //anim = gameObject.GetComponent<Animation>();
        animCtrl = gameObject.GetComponent<AnimationStateController>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (active)
        {
            if (!animator.enabled) animator.enabled = true;
            handleJump();
            movement();
        }
        /*
        if (Input.GetButtonDown("Jump"))
        {
            if (!controller.isGrounded && jumpsLeft > 0)
            {
                addJump();
            }
        }
        */
        else
        {
            if (animator.enabled) animator.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ICH");
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    void handleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                animCtrl.setJumping();
            }
            else if(!controller.isGrounded && jumpsLeft > 0)
            {
                addDoubleJump();
            }
        }
    }

    public void addJump()
    {
        if (!controller.isGrounded) return;
        yVelocity = jumpPower;
        playerSounds.PlayJumpSound();
        //playAnimation("metarig|jump");
    }

    //I'm assuming this was added so that a seperate animation can be used for jumping in midair
    public void addDoubleJump()
    {
        yVelocity = jumpPower;
        jumpsLeft--; //jumpsLeft refers to midair jumps
        playerSounds.PlayJumpSound();
    }

    void movement()
    {
        float xMove = Input.GetAxis("Horizontal") * speed; //Move player right/left

        if (!controller.isGrounded)
        {
            yVelocity -= gravity;
        }
        else
        {
            jumpsLeft = jumps - 1;
        }

        //If the input is moving the player right and the player is facing right
            if (xMove < 0 && facingRight)
        {
            Flip();
        }
        else if (xMove > 0 && !facingRight)
        {
            Flip();
        }

        controller.Move(new Vector3(xMove, yVelocity, 0) * Time.deltaTime);
    }

   

    void playAnimation(string str)
    {
        //anim.Play(str);
    }

    //Installing the flip here (Alex)
    
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0.0f, 180f, 0.0f);
    }

    



}