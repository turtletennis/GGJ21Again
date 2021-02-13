using UnityEngine;


public class GameSFX : MonoBehaviour
{
    private AudioSource GameSFXAudioEmitter;
    [SerializeField] AudioClip coinSound = null;
    [SerializeField] AudioClip levelEnd = null;


    // Start is called before the first frame update
    void Start()
    {
        GameSFXAudioEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayGameSFX(string _name)
    {
        

        if(coinSound == null) { return; }

        if (_name == "Coin" || _name == "coin")
        {
            GameSFXAudioEmitter.PlayOneShot(coinSound , 0.4f);
        }

        if (_name == "LevelEnd" || _name == "levelEnd")
        {
            GameSFXAudioEmitter.PlayOneShot(levelEnd, 0.4f);
        }




    }
}
