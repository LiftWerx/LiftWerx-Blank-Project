using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TriangleCounter : EditorWindow
{
    //open a window
    [MenuItem("Window/Count Triangles in Scene")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TriangleCounter>("Count Triangles In Scene");
    }

    private void OnGUI()
    {
        GUILayout.Label("Count Triangles in Scene", EditorStyles.boldLabel);

        if (GUILayout.Button("Count"))
        {
            CountTriangles();
        }
    }

    void CountTriangles()
    {
        int sceneTotalCount = 0;
        foreach (GameObject g in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            int triangleCount = 0;
            foreach (MeshFilter m in g.GetComponentsInChildren<MeshFilter>())
            {
                triangleCount += m.sharedMesh.triangles.Length;
            }
            Debug.Log(g.name + " has " + triangleCount.ToString() + " triangles");
            sceneTotalCount += triangleCount;
        }
        Debug.Log("Total triangles in the scene: "+ sceneTotalCount.ToString());
    }

}
