using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDesigner))]
public class Editor_ShapeGenrator : Editor
{
    private Material mat_Shape;
    private Material mat_Base;

    LevelDesigner levelDesigner;

   
    void OnEnable()
    {
        levelDesigner = target as LevelDesigner;
        mat_Shape = levelDesigner.mat_Shape;
        mat_Base = levelDesigner.mat_Base;

        SceneView.duringSceneGui += DuringSceneGUI;
        Debug.Log("OnEnable" );
    }

    void OnDisable()
    {
        Debug.Log("On Disable");
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    void DuringSceneGUI(SceneView sceneView)
    {
        if (target == null || !levelDesigner.gameObject.activeInHierarchy)
        {
            Debug.LogError("Need to have refrence in scene");
            return;
        }

        Event e = Event.current;
        if (e.type == EventType.MouseDown )
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                MeshRenderer meshRenderer = hitInfo.collider.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {

                    // Change the material
                    if (e.button == 0)
                    {
                        meshRenderer.material = mat_Shape;
                        Undo.RecordObject(meshRenderer, "Change Material");
                    }
                    else if (e.button == 1)
                    {
                        meshRenderer.material = mat_Base;
                        Undo.RecordObject(meshRenderer, "Change Material");
                    }
                    else
                    {
                        Debug.LogError("Either onClick 0 = mat_Shape or onClick=1 = mat_Base");
                    }

                }
            }
        }
    }
}
