using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Ambient3DSound : MonoBehaviour
{
    private AudioSource soundEmitter = null;

    // Start is called before the first frame update
    void Start()
    {
        if (soundEmitter == null)
        {
            return;
        }

        soundEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
    }

}
