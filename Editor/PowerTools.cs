using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Group))]

public class PowerTools : Editor
{
    private static Tool currentTool = Tools.current;
    private static bool spacePressed = false;
    private static bool pageDownPressed = false;

    static PowerTools()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }
    
    private static void OnSceneGUI(SceneView sceneView)
    {
        // Check for spacebar press
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space && !spacePressed)
        {
            CycleGizmo();
            spacePressed = true;
            e.Use();
        }
        if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Space)
        {
            spacePressed = false;
        }
        
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.PageDown && !pageDownPressed)
        {
            PlaceSelectedObjectsOnFloor();
            pageDownPressed = true;
            e.Use();
        }
        if (e.type == EventType.KeyUp && e.keyCode == KeyCode.PageDown)
        {
            pageDownPressed = false;
        }
    }
    
    private static void CycleGizmo()
    {
        switch (currentTool)
        {
            case Tool.Move:
                currentTool = Tool.Rotate;
                break;
            case Tool.Rotate:
                currentTool = Tool.Scale;
                break;
            case Tool.Scale:
                currentTool = Tool.Move;
                break;
        }
        Tools.current = currentTool;
    }
    
    private static void PlaceSelectedObjectsOnFloor()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            RaycastHit hit;
            if (Physics.Raycast(obj.transform.position, Vector3.down, out hit))
            {
                Undo.RecordObject(obj.transform, "Place " + obj.name + " on Floor");
                if (obj.GetComponent<Renderer>())
                {
                    obj.transform.position =
                        hit.point + new Vector3(0, obj.GetComponent<Renderer>()?.bounds.extents.y ?? 0, 0);
                }
                else
                {
                    obj.transform.position = hit.point;
                }
            }

        }
    }

    
    [MenuItem("Power Tools/Group Items %g")]
    private static void Group()
    {
        Undo.IncrementCurrentGroup();
        GameObject parentObj = new GameObject("Parent");
        Undo.RegisterCreatedObjectUndo(parentObj, "Create " + parentObj.name);
        
        GameObject[] objsToGroup = Selection.gameObjects;

        for (int i = 0; i < objsToGroup.Length; i++)
        {
            if (objsToGroup[i].transform.parent == null)
            {
                Undo.SetTransformParent(objsToGroup[i].transform, parentObj.transform, true, "Set Parent " + parentObj.name);
            }
            else
            {
                DestroyImmediate(parentObj);
            }
        }
    }
}