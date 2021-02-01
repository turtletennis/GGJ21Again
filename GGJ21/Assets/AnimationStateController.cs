using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isJumpingHash;

    public CharacterController controller;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool horizonalMovement = Input.GetKey("d") || Input.GetKey("a");
        bool jumpMovement = Input.GetButtonDown("Jump");



        if (!isWalking && horizonalMovement && controller.isGrounded)
        {
            animator.SetBool(isWalkingHash, true);  
        }

        if (isWalking && !horizonalMovement)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isJumping && jumpMovement)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isJumpingHash, true);
        }

        if (isJumping && !jumpMovement)
        {
            animator.SetBool(isJumpingHash, false);
        }


        //if (!isWalking && leftPress)
        //{
        //    animator.SetBool("isWalking", true);
        //}

        //if (isWalking && !leftPress)
        //{
        //    animator.SetBool("isWalking", false);
        //}


    }
}
