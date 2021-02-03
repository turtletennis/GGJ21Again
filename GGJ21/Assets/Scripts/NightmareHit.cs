using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightmareHit : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nightmare"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Update this when we get a death animation
        }
    }
}
