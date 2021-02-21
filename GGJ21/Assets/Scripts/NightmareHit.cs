using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareHit : MonoBehaviour
{
    private MusicManager2 musicManager;
    private CanvasScript cs;
    private ScoreTracker scoreTracker;

    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        musicManager = FindObjectOfType < MusicManager2>();
        scoreTracker = GetComponent<ScoreTracker>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nightmare"))
        {

            scoreTracker.ResetScoreToLevelStart();
            musicManager?.StopMusic();
            cs.playDeathFade();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Update this when we get a death animation
        }
    }
}
