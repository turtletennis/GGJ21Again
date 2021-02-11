using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]

public class NonPlayerSounds : MonoBehaviour
{
    [SerializeField]
    AudioClip CoinCollectSound = null;
    [SerializeField]
    AudioClip LevelEndSound = null;
    AudioSource soundEmitter;
    private int lastArrayPosition = 0;


    void Start()
    {
        soundEmitter = GetComponents<AudioSource>()[1];
        soundEmitter.volume = 1;
        

    }

    void Update()
    {

    }


    public void PlayCoinCollectSound()
    {
        soundEmitter.pitch = 1.0f;
        soundEmitter.PlayOneShot(CoinCollectSound);
    }

    public void PlayLevelEndSound()
    {
        soundEmitter.pitch = 1.0f;
        soundEmitter.PlayOneShot(LevelEndSound);
    }

    public bool IsSoundPlaying()
    {
        return soundEmitter.isPlaying;
    }

}



