using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishHit : MonoBehaviour
{
    private CanvasScript cs;
    GameSFX gameSFX;
    PlayerMovement playerMovement;
    AnimationStateController animationStateController;
    public string nextSceneName;
    private MusicManager2 musicManager;
    
    private bool hasFinishedLevel = false;
    private ScoreTracker scoreTracker;
    [SerializeField]
    int levelFinishScoreValue = 100;
    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        gameSFX = FindObjectOfType<GameSFX>();
        musicManager = FindObjectOfType<MusicManager2>();
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
            if (gameSFX == null)
            {
                gameSFX = FindObjectOfType<GameSFX>();
            }
            //remove double-jump ability if set
            hasFinishedLevel = true;
            scoreTracker.AddScore(levelFinishScoreValue);
            PlayerMovement.jumps = 1;
            gameSFX.PlayGameSFX("LevelEnd");
            //StartCoroutine(LevelFinish());
            SceneManager.LoadScene(nextSceneName);
            musicManager.StopMusic();
            hasFinishedLevel = false;


            //Update this when we get a death animation
        }
    }

    //IEnumerator LevelFinish()
    //{

    //    //yield return new WaitWhile(() => gameSFX.IsSoundPlaying());
        
    //}


}
