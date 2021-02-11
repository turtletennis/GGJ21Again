﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;

public class GameSFX : MonoBehaviour
{
    private AudioSource GameSFXAudioEmitter;
    [SerializeField] AudioClip coinSound;
   

    // Start is called before the first frame update
    void Start()
    {
        GameSFXAudioEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGameSFX(string _name)
    {
        if (_name == "Coin" || _name == "coin")
        {
             GameSFXAudioEmitter.PlayOneShot(coinSound);
        }
       
    }
}
