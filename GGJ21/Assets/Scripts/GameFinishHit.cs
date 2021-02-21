using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishHit : MonoBehaviour
{
    private CanvasScript cs;
    GameSFX gameSFX;
    public string startSceneName;
    private MusicManager2 musicManager;
    
    private bool hasFinishedLevel = false;
    private ScoreTracker scoreTracker;
    
    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        gameSFX = FindObjectOfType<GameSFX>();
        musicManager = FindObjectOfType<MusicManager2>();
        scoreTracker = GetComponent<ScoreTracker>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Restart") && !hasFinishedLevel)
        {
            if (gameSFX == null)
            {
                gameSFX = FindObjectOfType<GameSFX>();
            }
            //remove double-jump ability if set
            hasFinishedLevel = true;
            
            PlayerMovement.ResetPowers();
            gameSFX?.PlayGameSFX("LevelEnd");
            //StartCoroutine(LevelFinish());
            SceneManager.LoadScene(startSceneName);
            musicManager.StopMusic();
            scoreTracker.ResetScoreToZero();
            hasFinishedLevel = false;


            //Update this when we get a death animation
        }else if (other.gameObject.CompareTag("GameEnd") && !hasFinishedLevel)
        {
            
            Application.Quit();
        }
    }

    //IEnumerator LevelFinish()
    //{

    //    //yield return new WaitWhile(() => gameSFX.IsSoundPlaying());
        
    //}


}
