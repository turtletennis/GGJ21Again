using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private GameSFX gameSFX;
    private CanvasScript canvas;
    private ScoreTracker scoreTracker;
    [SerializeField]
    int coinScoreValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        gameSFX = FindObjectOfType<GameSFX>();
        canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        scoreTracker = GetComponent<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("coin"))
        {
            scoreTracker.AddScore(coinScoreValue);
            GameObject.Destroy(other.gameObject);
            gameSFX.PlayGameSFX("coin");
        }
    }
}
