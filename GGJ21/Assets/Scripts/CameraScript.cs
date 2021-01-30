using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;

    public float zoom;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = target.transform.position + Vector3.back * zoom;
    }
}
