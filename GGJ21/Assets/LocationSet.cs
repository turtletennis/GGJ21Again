using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSet : MonoBehaviour
{
    public string location = "Beach";
    [SerializeField] GameObject playerSounds = null;



    // Start is called before the first frame update
    void Start()
    {

        if(playerSounds ==null) { return; }
        
        if(location == "Beach")
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
