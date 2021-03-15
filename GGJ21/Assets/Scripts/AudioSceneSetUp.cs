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
        if (_myMusicManager != null)
        {
            _myMusicManager.StartMusicOnLevelLoad(musicForLevel);
        }
        else
        {
            Debug.Log("Missing Music Manager!");
        }
        _myAmbManager = FindObjectOfType<AmbienceManager>();
        if (_myAmbManager != null)
        {
            _myAmbManager.StartAmbienceOnLevelLoad(ambienceForLevel);
        }
        else
        {
            Debug.Log("Missing Ambience Manager!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
