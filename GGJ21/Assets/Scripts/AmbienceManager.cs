using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbienceManager : MonoBehaviour
{
    private AudioSource ambienceSoundEmitter;
    //[SerializeField] AudioClip ambienceSFXLoop = null;
    
    // Start is called before the first frame update
    void Start()
    {
        ambienceSoundEmitter = GetComponent<AudioSource>();
        
            //ambienceSoundEmitter.loop = true;
            //ambienceSoundEmitter.clip = ambienceSFXLoop;
            //ambienceSoundEmitter.Play();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAmbienceOnLevelLoad(AudioClip _audioClip)
    {
        ambienceSoundEmitter.loop = true;
        ambienceSoundEmitter.clip = _audioClip;
        ambienceSoundEmitter.Play();
    }
}
