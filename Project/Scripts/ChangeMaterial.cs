using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material SelectMaterial;
    public Material FinalMaterial;
    public Material CurrentMaterial;
    // Update is called once per frame
    public void ChangeMaterialOnSelection()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = SelectMaterial;
    }
    public void ChangeMaterialAfterSelection()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = FinalMaterial;
    }
    public void ChangeMaterialBeforeSelection()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = CurrentMaterial;
    }
}
