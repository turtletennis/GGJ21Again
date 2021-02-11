using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbienceManager : MonoBehaviour
{
    private AudioSource ambienceSoundEmitter;
    [SerializeField] AudioClip ambienceSFXLoop;
    [SerializeField] string Level = "PlayRoom";
    // Start is called before the first frame update
    void Start()
    {
        ambienceSoundEmitter = GetComponent<AudioSource>();
        if(Level == "Beach")
        {
            ambienceSoundEmitter.loop = true;
            ambienceSoundEmitter.clip = ambienceSFXLoop;
            ambienceSoundEmitter.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
