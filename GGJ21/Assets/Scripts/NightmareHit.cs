using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareHit : MonoBehaviour
{
    private CanvasScript cs;
    private ScoreTracker scoreTracker;

    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
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
            cs.playDeathFade();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Update this when we get a death animation
        }
    }
}
