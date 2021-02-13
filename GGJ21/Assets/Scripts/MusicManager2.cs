using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
class MusicManager2 : MonoBehaviour
{
    private AudioSource musicEmitter;
    // Start is called before the first frame update
    void Start()
    {
        musicEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StopMusic()
    {
        musicEmitter.Stop();
    }

    public void StartMusicOnLevelLoad(AudioClip _audioClip)
    {
        //.
        if (_audioClip == null) { return; }
        musicEmitter.loop = true;
        musicEmitter.clip = _audioClip;
        musicEmitter.Play();
    }
}
