using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BlockCollide : MonoBehaviour
{
    private AudioSource  soundEmitter;
    [SerializeField] AudioClip bounceSound;
   
    // Start is called before the first frame update
    void Start()
    {
        soundEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if((collision.relativeVelocity.magnitude >  (4 , 4 , 4)) { return; }


        Debug.Log("Collisionblocks");
        soundEmitter.clip = bounceSound;
        soundEmitter.Play();
    }
}
