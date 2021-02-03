using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareFollow : MonoBehaviour
{
    public float range;
    public float speed;
    public float heightOffset; //When set to 0, the nightmare homes in on the player's feet

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("character");
    }

    void Update()
    {
        if (PlayerMovement.active)
        {
            Vector3 playerPosition = player.transform.position + new Vector3(0, heightOffset, 0);
            if (Mathf.Abs(Vector3.Distance(playerPosition, gameObject.transform.position)) <= range)
            {
                Vector3 moveVector = (playerPosition - gameObject.transform.position).normalized;
                gameObject.transform.Translate(moveVector * speed * Time.deltaTime);
            }
        }
    }
}
