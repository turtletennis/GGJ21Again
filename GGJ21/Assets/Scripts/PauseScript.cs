using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    void Start()
    {
        setPaused(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            setPaused(true);
        }
    }

    void setPaused(bool paused)
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(paused);
        }

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void unpause()
    {
        setPaused(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
