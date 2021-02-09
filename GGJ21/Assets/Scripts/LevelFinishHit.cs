using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishHit : MonoBehaviour
{
    private CanvasScript cs;
    public string nextSceneName;

    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            
            SceneManager.LoadScene(nextSceneName);
            //Update this when we get a death animation
        }
    }
}
