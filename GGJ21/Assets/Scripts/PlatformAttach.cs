using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    private GameObject target = null;
    private PlayerMovement playerMovement = null;
    private Vector3 offset;
    private Vector3 previousPosition=Vector3.zero;
    private Vector3 platformLandOffset = Vector3.zero;

    private void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == Player)
        {
            

            if (target==null)
            {   //just landed on the platform
                previousPosition = transform.position;
                platformLandOffset = previousPosition - transform.position;
            }
            target = other.gameObject;
            playerMovement.PlatformOffset = (transform.position-previousPosition)/Time.deltaTime;
            previousPosition = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            target = null;
            playerMovement.PlatformOffset = Vector3.zero;
        }
    }
    //private void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        target.transform.position += offset;
    //    }
    //}


}
