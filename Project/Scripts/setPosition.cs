using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPosition : MonoBehaviour
{
    public GameObject NacellePos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = NacellePos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
