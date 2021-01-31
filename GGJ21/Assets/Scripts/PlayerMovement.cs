using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.PlayerSounds;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed;
    public float jumpPower;
    public float gravity;
    public int jumps = 1;

    private float yVelocity;
    private int jumpsLeft = 0; //Number of midair jumps left (default = jumps - 1)

    public playerSounds playerSounds;
        

    

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * speed; //Move player right/left

        //Vertical movement
        //Jumping
        //Note, the jump button is space bar, not the up key... (A.E)
        if(Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                yVelocity = jumpPower;

                //SH play JUMP sound
                playerSounds.PlayJumpSound();


            }
            else if(jumpsLeft > 0)
            {
                jumpsLeft--;
                yVelocity = jumpPower;
            }
        }
        //Falling
        if (controller.isGrounded)
        {
            jumpsLeft = jumps - 1;
        }
        else
        {
            yVelocity -= gravity;
        }

        controller.Move(new Vector3(xMove, yVelocity, 0) * Time.deltaTime);
    }
}
