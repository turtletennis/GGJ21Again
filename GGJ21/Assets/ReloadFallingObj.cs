using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadFallingObj : MonoBehaviour
{
    [SerializeField] Vector3 respawnPosition;
    [SerializeField] Quaternion respawnRotation;
 
    public GameObject blockPrefab;

    //public GameObject playerPefab;

    [SerializeField] Vector3 respawnPlayerPosition;
    [SerializeField] Quaternion respawnPlayerRotation;

    [SerializeField] GameObject playereRespawnPosition;
    //[SerializeField] Quaternion respawnRotation;


    // Start is called before the first frame update
    void Start()
    {
        
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


}   
