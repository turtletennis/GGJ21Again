using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum Ability
    {
        NULL,
        DOUBLE_JUMP
    }

    public void setAbility(Ability ability)
    {
        switch (ability)
        {
            case Ability.DOUBLE_JUMP:
                gameObject.GetComponent<PlayerMovement>().jumps = 2;
                break;
        }
    }
}
