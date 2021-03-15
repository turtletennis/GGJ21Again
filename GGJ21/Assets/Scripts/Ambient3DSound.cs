using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Ambient3DSound : MonoBehaviour
{
    [SerializeField] GameObject character;
    private AudioSource soundEmitterA = null;

    [SerializeField] AudioClip sFXToPlayA = null;

    // Start is called before the first frame update
    void Start()
    {


        soundEmitterA = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject == character)
        //{

        if (soundEmitterA.isPlaying)
        {
            return;
        }

        soundEmitterA.loop = true;
        soundEmitterA.clip = sFXToPlayA;
        soundEmitterA.Play();

        //Debug.Log("TE");
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        soundEmitterA.Stop();
        //}
    }
}


