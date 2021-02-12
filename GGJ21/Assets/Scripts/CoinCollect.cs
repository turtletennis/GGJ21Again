using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private GameSFX gameSFX;
    // Start is called before the first frame update
    void Start()
    {
        gameSFX = FindObjectOfType<GameSFX>();
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
            gameSFX.PlayGameSFX("Coin");
        }
    }
}
