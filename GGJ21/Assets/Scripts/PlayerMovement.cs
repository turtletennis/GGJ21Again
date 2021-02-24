using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private AnimationStateController animCtrl; //Calls changes to animations so that inputs for the player character are all contained in PlayerMovement (and Abilities)

    public float speed;
    public float jumpPower;
    public static bool boostJump = false;
    public static float boostJumpMultiplier = 1.5f;
    public float gravity;
    public static int jumps = 1;
    
    public static bool active = true;
    private bool facingRight = true; //This will indicate the char is facing right
    public float flipRotationSpeeed = 0.2f;
    public bool lerpFlipRotation = true;

    public Animator animator;

    private float yVelocity;
    private int jumpsLeft = 0; //Number of midair jumps left (default = jumps - 1)
    private bool isJumpReady = true; //Needed for double jumps

    public bool isWalking;

    private playerSounds playerSounds;

    public static void ResetPowers()
    {
        jumps = 1;
        boostJump = false;
    }

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        //anim = gameObject.GetComponent<Animation>();
        animCtrl = gameObject.GetComponent<AnimationStateController>();
        animator = GetComponent<Animator>();
        playerSounds = GetComponent<playerSounds>();


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
        if (Input.GetButton("Jump"))
        {
            if (controller.isGrounded)
            {
                animCtrl.setJumping();
                isJumpReady = false;
            }
            else if(!controller.isGrounded && jumpsLeft > 0 && isJumpReady)
            {
                addDoubleJump();
            }
            else if(isJumpReady)
            {
                Debug.Log("Not grounded!");
            }
        }

        //Reset jump key to allow double jumps
        if (Input.GetButtonUp("Jump"))
        {
            isJumpReady = true;
        }
    }

    public void addJump()
    {
        if (!controller.isGrounded) return;
        yVelocity = jumpPower * (boostJump ? boostJumpMultiplier:1);
        playerSounds.PlayJumpSound();
        //playAnimation("metarig|jump");
    }

    //I'm assuming this was added so that a seperate animation can be used for jumping in midair
    public void addDoubleJump()
    {
        yVelocity = jumpPower * (boostJump ? boostJumpMultiplier : 1);
        jumpsLeft--; //jumpsLeft refers to midair jumps
        playerSounds.PlayJumpSound();
    }

    void movement()
    {
        float xMove;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|jump land"))
        {
            //controller.Move(new Vector3(xMove, yVelocity, 0) * Time.deltaTime );
            //controller.Move(new Vector3(xMove, yVelocity, 0) * 0);
            xMove = 0;

            //return;
        }
        else
        {
            xMove = Input.GetAxis("Horizontal") * speed; //Move player right/left
        }

       

        if (!controller.isGrounded)
        {
            yVelocity -= gravity*Time.deltaTime;
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

        controller.Move(new Vector3(xMove, yVelocity, 0)  * Time.deltaTime);
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
        //transform.Rotate(0.0f, 180f, 0.0f);
        if (lerpFlipRotation)
        {
            if (facingRight)
            {
                StartCoroutine(LerpFunction(Quaternion.Euler(0, 90, 0), flipRotationSpeeed));
            }

            if (!facingRight)
            {
                StartCoroutine(LerpFunction(Quaternion.Euler(0, 270, 0), flipRotationSpeeed));
            }
        }
        else
        {
            transform.Rotate(0.0f, 180f, 0.0f);
        }

    }   

    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
    }
}

    



