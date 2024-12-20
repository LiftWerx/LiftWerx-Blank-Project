using UnityEngine;

public class ChangeChildren : MonoBehaviour
{
    public Material newMaterial; // The material to apply to the children

    void Start()
    {
        
    }

    // Function to change materials of children recursively
    public void ChangeMaterialsRecursively(Transform parent)
    {
        // Iterate through all children of the parent transform
        foreach (Transform child in parent)
        {
            // Get the Renderer component of the child (if it exists)
            Renderer renderer = child.GetComponent<Renderer>();

            // If a Renderer component is found, change its material
            if (renderer != null)
            {
                // Change the material of the renderer to the new material
                renderer.material = newMaterial;
            }

            // Recursively call this function for each child to change their materials
            ChangeMaterialsRecursively(child);
        }
    }
}

