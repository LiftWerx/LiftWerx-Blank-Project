using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {   
        if(spawn != null){
            transform.position = new Vector3(spawn.transform.position.x, transform.position.y, spawn.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
