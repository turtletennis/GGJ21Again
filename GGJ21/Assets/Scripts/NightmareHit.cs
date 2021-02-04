using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareHit : MonoBehaviour
{
    private CanvasScript cs;

    void Start()
    {
        cs = GameObject.Find("Canvas").GetComponent<CanvasScript>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nightmare"))
        {
            cs.playDeathFade();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Update this when we get a death animation
        }
    }
}
