using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryHit : MonoBehaviour
{
    private CharacterController controller;
    private Abilities ab;
    private CanvasScript cs;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        ab = gameObject.GetComponent<Abilities>();
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MemorySettings mem = other.gameObject.GetComponent<MemorySettings>();
        if(mem)
        {
            PlayerMovement.active = false;
            cs.showMemory(mem.image, mem.scene);
            ab.setAbility(mem.ability);
            GameObject.Destroy(other.gameObject);
        }
    }
}
