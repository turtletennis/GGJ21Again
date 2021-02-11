using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishHit : MonoBehaviour
{
    private CanvasScript cs;
    playerSounds playerSounds;
    public string nextSceneName;

    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        playerSounds = GetComponent<playerSounds>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            //remove double-jump ability if set
            PlayerMovement.jumps = 1;
            playerSounds.PlayLevelEndSound();
            SceneManager.LoadScene(nextSceneName);
            //Update this when we get a death animation
        }
    }
}
