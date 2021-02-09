using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class EnvironmentSounds : MonoBehaviour
{
    [SerializeField] Vector3 respawnPosition;
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
    }

    // Update is called once per fram
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Floor"))
        {
            //if (collision.relativeVelocity.magnitude > 4)
            //{
            //Debug.Log("AAA " + collision.relativeVelocity.magnitude);
            //}
        }
    }
}



