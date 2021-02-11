using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    NonPlayerSounds nonPlayerSounds;
    // Start is called before the first frame update
    void Start()
    {
        nonPlayerSounds = GetComponent<NonPlayerSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("coin"))
        {
            GameObject.Destroy(other.gameObject);
            nonPlayerSounds.PlayCoinCollectSound();
        }
    }
}
