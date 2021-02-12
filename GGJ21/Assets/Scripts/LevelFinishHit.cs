using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishHit : MonoBehaviour
{
    private CanvasScript cs;
    public string nextSceneName;
    private MusicManager2 musicManager;
    

    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        musicManager = FindObjectOfType<MusicManager2>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            //remove double-jump ability if set
            musicManager.StopMusic();
            PlayerMovement.jumps = 1;
            SceneManager.LoadScene(nextSceneName);
            //Update this when we get a death animation
        }
    }
}
