using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.PlayerSounds;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animation anim;

    public float speed;
    public float jumpPower;
    public float gravity;
    public static int jumps = 1;
    public static bool active = true;

    private float yVelocity;
    private int jumpsLeft = 0; //Number of midair jumps left (default = jumps - 1)

    public playerSounds playerSounds;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        if(active)
        {
            moveAndJump();
            if (!anim.isPlaying) anim.Play();
        }
        else
        {
            anim.Stop();
        }
    }

    void moveAndJump()
    {
        float xMove = Input.GetAxis("Horizontal") * speed; //Move player right/left

        //Vertical movement
        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                yVelocity = jumpPower;
                playerSounds.PlayJumpSound();
                playAnimation("metarig|jump");
            }
            else if (jumpsLeft > 0)
            {
                jumpsLeft--;
                yVelocity = jumpPower;
            }
        }
        //Falling
        if (controller.isGrounded && !Input.GetButtonDown("Jump"))
        {
            jumpsLeft = jumps - 1;
            playAnimation("metarig|walk");
            //yVelocity = 0;
        }
        else
        {
            yVelocity -= gravity;
        }

        //if (controller.isGrounded)
        //{
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        yVelocity = jumpPower;
        //        playerSounds.PlayJumpSound();
        //    }
        //    else
        //    {
        //        jumpsLeft = jumps - 1;
        //        yVelocity = 0;
        //    }
        //}
        //else
        //{
        //    if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        //    {
        //        jumpsLeft--;
        //        yVelocity = jumpPower;
        //    }
        //    else
        //    {
        //        yVelocity -= gravity;
        //    }
        //}

        controller.Move(new Vector3(xMove, yVelocity, 0) * Time.deltaTime);
    }

    public void playFootstepL()
    {
        playerSounds.PlayFootStepSound();
        Debug.Log("footsteep triggered");
    }

    public void playFootstepR()
    {
        playerSounds.PlayFootStepSound();
        Debug.Log("footsteep triggered");
    }

    void playAnimation(string str)
    {
        anim.Play(str);
    }
}
