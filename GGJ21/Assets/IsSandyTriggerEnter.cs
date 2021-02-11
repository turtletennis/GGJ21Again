using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSandyTriggerEnter : MonoBehaviour
{
    public GameObject character;
    private playerSounds characterPlayerSounds;
    public bool makeTransitionOnEnterSandy;

    // Start is called before the first frame update
    void Start()
    {
        characterPlayerSounds = character.GetComponent<playerSounds>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(makeTransitionOnEnterSandy == true)
        {
            if (other.CompareTag("Respawn"))
            {
                characterPlayerSounds.ifSand = true;
            }

        }
        else
        {
            if (other.CompareTag("Respawn"))
            {
                characterPlayerSounds.ifSand = false;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (makeTransitionOnEnterSandy == true)
        {
            if (other.CompareTag("Respawn"))
            {
                characterPlayerSounds.ifSand = false;
            }

        }
        else
        {
            if (other.CompareTag("Respawn"))
            {
                characterPlayerSounds.ifSand = true;
            }
        }
    }
}
