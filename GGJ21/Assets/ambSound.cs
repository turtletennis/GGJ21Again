using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ambSound : MonoBehaviour
{
  private AudioSource ambSoundEmitter = null;
    [SerializeField] AudioClip blockCollidingSound = null;
    private double timeLastPlayed;

    // Start is called before the first frame update
    void Start()
    {
        ambSoundEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBlock()
    {
        //double playDelay = timeLastPlayed - AudioSettings.dspTime;


        if (ambSoundEmitter == null || blockCollidingSound == null)
        {
            //Debug.Log("S");
            return;
        }
        //Debug.Log("S");
        if(ambSoundEmitter.isPlaying)
        {
            return;
        }
        ambSoundEmitter.clip = blockCollidingSound;
        ambSoundEmitter.pitch = 0.7f;
        ambSoundEmitter.Play();
        //timeLastPlayed = AudioSettings.dspTime;
    }
}
