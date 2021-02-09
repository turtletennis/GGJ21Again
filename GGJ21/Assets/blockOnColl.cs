using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class blockOnColl : MonoBehaviour
{

    private ambSound ambSound = null; 

    // Start is called before the first frame update
    void Start()
    {
        ambSound = FindObjectOfType<ambSound>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.gameObject.CompareTag("Floor"))
        {
            if(collision.relativeVelocity.magnitude > 15)
            {

                ambSound.PlayBlock();
            }
        }
    }
}
