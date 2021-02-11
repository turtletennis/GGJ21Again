using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSandyTriggerEnter : MonoBehaviour
{
    public GameObject character;
    private playerSounds characterPlayerSounds;

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
        if (other.CompareTag("Respawn"))
        {
        characterPlayerSounds.ifSand = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            characterPlayerSounds.ifSand = true;
            }
    }
}
