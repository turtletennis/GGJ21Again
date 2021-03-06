﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishHit : MonoBehaviour
{
    private CanvasScript cs;
    NonPlayerSounds nonPlayerSounds;
    PlayerMovement playerMovement;
    AnimationStateController animationStateController;
    public string nextSceneName;
    private bool hasFinishedLevel = false;
    private ScoreTracker scoreTracker;
    [SerializeField]
    int levelFinishScoreValue = 100;
    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        nonPlayerSounds = GetComponent<NonPlayerSounds>();
        playerMovement = GetComponent<PlayerMovement>();
        animationStateController= GetComponent<AnimationStateController>();
        scoreTracker = GetComponent<ScoreTracker>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelEnd") && !hasFinishedLevel)
        {
            if (nonPlayerSounds == null)
            {
                nonPlayerSounds = GetComponent<NonPlayerSounds>();
            }
            //remove double-jump ability if set
            hasFinishedLevel = true;
            scoreTracker.AddScore(levelFinishScoreValue);
            PlayerMovement.jumps = 1;
            nonPlayerSounds.PlayLevelEndSound();
            StartCoroutine(LevelFinish());
            
            
            //Update this when we get a death animation
        }
    }

    IEnumerator LevelFinish()
    {
        
        yield return new WaitWhile(() => nonPlayerSounds.IsSoundPlaying());
        SceneManager.LoadScene(nextSceneName);
        hasFinishedLevel = false;
    }


}
