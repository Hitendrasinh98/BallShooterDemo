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

    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    void DuringSceneGUI(SceneView sceneView)
    {
        if (target == null || !levelDesigner.gameObject.activeInHierarchy)
        {
            Debug.LogError("Need to have this level designer scirpt  in scene");
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

    public override void OnInspectorGUI()
    {



        // Style for the title
        GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.fontSize = 20;
        titleStyle.richText = true;
        titleStyle.normal.textColor = new Color(0.1f, 0.4f, 0.8f); // Custom color

        // Display the title
        GUILayout.Label("<b>Shape Designer Tool</b>", titleStyle);

        GUILayout.Space(20);

        base.OnInspectorGUI();

        GUILayout.Space(20);

        GUILayout.Label("Instructions:", EditorStyles.boldLabel);
        GUILayout.Label("- Lock this Inspector to prevent accidental changes.", EditorStyles.wordWrappedLabel);
        GUILayout.Label("- Use the Left Mouse Click to apply Shape Material.", EditorStyles.wordWrappedLabel);
        GUILayout.Label("- Use the Right Mouse Click to Reset Material.", EditorStyles.wordWrappedLabel);

    }

}
