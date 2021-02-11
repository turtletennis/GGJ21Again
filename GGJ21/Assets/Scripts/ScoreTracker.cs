using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private static int score;
    private CanvasScript cs;
    // Start is called before the first frame update
    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        cs.SetScoreText(score);
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
