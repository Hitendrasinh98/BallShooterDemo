using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

/// <summary>
/// Custom editor for Shape Manager to have certain features
/// like to  see the json data and convert json data
/// have color aplied onto the jsonData shapes AvailbelColors to visualize it
/// </summary>

[CustomEditor(typeof(So_ShapeManager))]
public class Editor_ShapeManager : Editor
{
    private Vector2 scrollPosition = Vector2.zero;
    private GUIStyle customButtonStyle;

    private void OnEnable()
    {
        try
        {
            customButtonStyle = new GUIStyle(EditorStyles.miniButton);
        }
        catch(System.Exception e)
        {
            Debug.Log("Got error :" + e.Message);
            Debug.LogError("try ,reselecting this asset " );

            return;
        }
        customButtonStyle.normal.background = MakeTexture(new Color(0.25f, 0.25f, 0.25f, 1f));
        customButtonStyle.normal.textColor = Color.white;
        customButtonStyle.active.background= MakeTexture(new Color(0.0f, .5f, 0.0f, 1f));
        customButtonStyle.active.textColor = Color.white;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        So_ShapeManager yourComponent = (So_ShapeManager)target;
        GUILayout.Space(50);

        if (GUILayout.Button("Genrate Json", customButtonStyle))
        {
            yourComponent.SaveIntoJson();
        }


        GUIStyle coloredTextStyle = new GUIStyle(EditorStyles.textArea);
        coloredTextStyle.richText = true;

        string formattedText =yourComponent.letestJsonData;

        GUILayout.Label("JsonData:");
        int totalLinesOfData = formattedText.Split('\n').Length;
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(EditorGUIUtility.singleLineHeight * totalLinesOfData));
        formattedText = EditorGUILayout.TextArea(formattedText, coloredTextStyle, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        yourComponent.letestJsonData = formattedText;

        serializedObject.ApplyModifiedProperties();
    }

    // Utility function to create a colored texture
    private Texture2D MakeTexture(Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return texture;
    }
}
