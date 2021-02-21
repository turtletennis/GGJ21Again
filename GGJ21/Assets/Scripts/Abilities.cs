using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum Ability
    {
        NULL,
        DOUBLE_JUMP,
        MEGA_JUMP,
        UNHIDE
    }

    private CharacterController controller;

    private static bool unhideFound = false;
    private MeshRenderer[] hiddenObjects;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Hidden");
        hiddenObjects = new MeshRenderer[objs.Length];
        for(int i=0; i<objs.Length; i++)
        {
            hiddenObjects[i] = objs[i].GetComponent<MeshRenderer>();
        }
    }

    void Update()
    {
        if (unhideFound)
        {
            if (Input.GetButtonDown("Fire1") && controller.isGrounded)
            {
                useUnhide(true);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                useUnhide(false);
            }
        }
    }

    public void setAbility(Ability ability)
    {
        switch (ability)
        {
            case Ability.DOUBLE_JUMP:
                PlayerMovement.jumps = 2;
                break;
            case Ability.UNHIDE:
                unhideFound = true;
                break;
            case Ability.MEGA_JUMP:
                PlayerMovement.jumps = 2;
                PlayerMovement.boostJump = true;
                break;
        }
    }

    //While the key mapped to fire1 is pressed, objects marked with tag Hidden are shown, they hide again when released
    void useUnhide(bool active)
    {
        PlayerMovement.active = !active;
        foreach (MeshRenderer rend in hiddenObjects)
        {
            if(rend) rend.enabled = active;
        }
        //Play an animation here
    }
}
