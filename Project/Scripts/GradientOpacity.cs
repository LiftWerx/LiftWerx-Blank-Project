using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GradientOpacity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color newColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% transparency
        gameObject.GetComponent<Renderer>().material.color = newColor;
    }
}

