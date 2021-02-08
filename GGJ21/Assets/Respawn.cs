using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject playerPefab;
    [SerializeField] GameObject respawnPosition;
    private Vector3 respawnPositionV3;
    [SerializeField] Quaternion respawnRotation;

    // Start is called before the first frame update
    void Start()
    {
    //    respawnPositionV3 = respawnPosition.transform.position;
    //    respawnRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
    
     //if (other.gameObject.CompareTag("Respawn"))
     //   {

     //       //SpawnBlock();
     //       Debug.Log("HHHH");
     //       //newBlock.transform.position = respawnPosition;
     //       ///Instantiate(playerPefab, respawnPosition, respawnRotation);
     //       //respawnPosition.z = respawnPosition.z + 10;
     //       gameObject.transform.position = respawnPositionV3;
     //       //Destroy(gameObject);
     //   }
}
}
