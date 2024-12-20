using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public GameObject SolidObject;
    public GameObject Hologram;
    // Start is called before the first frame update
    public void Build()
    {
        SolidObject.transform.position = Hologram.transform.position;
    }
}
