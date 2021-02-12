using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSceneSetUp : MonoBehaviour
{
    public AudioClip ambienceForLevel;
    public AudioClip musicForLevel;
    private MusicManager2 _myMusicManager;
    private AmbienceManager _myAmbManager;

    // Start is called before the first frame update
    void Start()
    {
        _myMusicManager = FindObjectOfType<MusicManager2>();
        _myMusicManager.StartMusicOnLevelLoad(musicForLevel);
        _myAmbManager = FindObjectOfType<AmbienceManager>();
        _myAmbManager.StartAmbienceOnLevelLoad(ambienceForLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
