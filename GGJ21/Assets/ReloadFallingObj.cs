using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ReloadFallingObj : MonoBehaviour
{
    private Vector3 respawnPosition;
    [SerializeField] Quaternion respawnRotation;
 
    public GameObject blockPrefab;

    //public GameObject playerPefab;

    [SerializeField] Vector3 respawnPlayerPosition;
    [SerializeField] Quaternion respawnPlayerRotation;

    [SerializeField] GameObject playereRespawnPosition;
    //[SerializeField] Quaternion respawnRotation;

    private AudioSource soundEmitter;


    // Start is called before the first frame update
    void Start()
    {
        soundEmitter = GetComponent<AudioSource>();
        //respawnPosition = playereRespawnPosition.transform.position;
        //respawnRotation = playereRespawnPosition.transform.rotation;
    }

    // Update is called once per fram
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (blockPrefab == null) { return; }
        if (other.CompareTag("Blocks"))
        {

            //SpawnBlock();

            //newBlock.transform.position = respawnPosition;

            



            Instantiate(blockPrefab, respawnPosition , respawnRotation);
            //respawnPosition.z = respawnPosition.z + 10;

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.transform.position = playereRespawnPosition.transform.position;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(gameObject.CompareTag("Floor"))
        //{
        //    if(collision.relativeVelocity.magnitude > 4)


        //    Debug.Log("AAA" + collision.relativeVelocity.magnitude);
        //}
    }


}   
