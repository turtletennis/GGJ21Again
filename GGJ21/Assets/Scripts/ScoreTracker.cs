using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private static int score;
    private CanvasScript cs;
    private int scoreAtLevelStart;
    // Start is called before the first frame update
    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        cs.SetScoreText(score);
        scoreAtLevelStart = score;
    }
    public void ResetScoreToLevelStart()
    {
        score = scoreAtLevelStart;
    }
    public void AddScore(int diff)
    {
        score += diff;
        cs.SetScoreText(score);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
