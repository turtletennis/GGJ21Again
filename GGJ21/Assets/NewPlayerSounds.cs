using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NewPlayerSounds : MonoBehaviour
{
    [SerializeField] bool isBeach;
    [SerializeField] bool isHardFloor;

    [SerializeField] AudioClip[] beachFootsteps;
    [SerializeField] AudioClip[] hardFootsteps;
    [SerializeField] float fsPitchMin = 1;
    [SerializeField] float fsPitchMax = 1;
    [SerializeField] bool fsDoNotRepeat = true;

    //[SerializeField] AudioClip[] beachJumpSounds;
    //[SerializeField] AudioClip[] hardJumpSounds;

    //[SerializeField] float jumpPitchMin = 1;
    //[SerializeField] float jumpPitchMax = 1;
    //[SerializeField] bool jumpDoNotRepeat = true;


    //[SerializeField] AudioClip[] beachLandingSounds;
    //[SerializeField] AudioClip[] hardLandingSounds;

    //[SerializeField] float landingPitchMin = 1;
    //[SerializeField] float landingPitchMax = 1;
    //[SerializeField] bool landingDoNotRepeat = true;

    //[SerializeField]
    //AudioClip[] dBLJumpSounds;
    //[SerializeField]
    //float dBLJumpPitchMin = 1;
    //[SerializeField]
    //float dBLJumpPitchMax = 1;
    //[SerializeField]
    //bool dBLJumpDoNotRepeat = true;

    [SerializeField] bool playerSoundsDebug;

    private AudioSource soundEmitter;
    private int lastArrayPosition;



    // Start is called before the first frame update
    void Start()
    {
        soundEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isBeach);
    }

    public void FSEventR()
    {
        PlayFootStepSound(beachFootsteps);
    }

    public void FSEventL()
    {
        Debug.Log("Play HUH");
        PlayFootStepSound(beachFootsteps);
        Debug.Log("Play HUH");
    }

    public void PlayFootStepSound(AudioClip[]  _footPrintArray)
    {

        
        if (_footPrintArray.Length == 0)
        {
            if (playerSoundsDebug)
            {
                Debug.Log("beachFootsteps[] is empty - RETURN");
            }
            return;
        }

        if (soundEmitter.isPlaying)
        {
            soundEmitter.Stop();
        }

        //RadomizePitch(fsPitchMin, fsPitchMax);
        //ChooseSound(beachFootsteps);
        soundEmitter.clip = _footPrintArray[1];
        soundEmitter.Play();
        //NoRepeat(fsDoNotRepeat, beachFootsteps);


        if (playerSoundsDebug)
        {
            Debug.Log("Play HUH");
        }
    }
    //private void RadomizePitch(float PitchMin, float PitchMax)
    //{
    //    float pitch = Random.Range(PitchMin, PitchMax);
    //    soundEmitter.pitch = pitch;
    //}
    //private void ChooseSound(AudioClip[] soundArray)
    //{

    //    lastArrayPosition = Random.Range(1, soundArray.Length);
    //    soundEmitter.clip = soundArray[lastArrayPosition];
    //}

    //private void NoRepeat(bool noRepeat, AudioClip[] soundArray)
    //{
    //    if (noRepeat)
    //    {
    //        soundArray[lastArrayPosition] = soundArray[0];
    //        soundArray[0] = soundEmitter.clip;
    //    }
    //}
}
