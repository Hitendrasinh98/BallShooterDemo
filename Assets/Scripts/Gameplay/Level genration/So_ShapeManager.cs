using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/ShapeManager",order =0)]
public class So_ShapeManager : ScriptableObject
{
    [SerializeField] List<So_Shape> mShapes;

    [HideInInspector]public string letestJsonData;

    public void SaveIntoJson()
    {
        List<SerializableShape> serializableShapes = new List<SerializableShape>();
        foreach (So_Shape shape in mShapes)
        {
            serializableShapes.Add(new SerializableShape(shape.GetShapeType() , GetAvailableColorsHex( shape.GetAvailableColors())));
        }

        string[] jsonData = new string[serializableShapes.Count];
        string debugJson = "";
        for (int i = 0; i < serializableShapes.Count; i++)
        {
            jsonData[i] =  JsonUtility.ToJson(serializableShapes[i]);
            debugJson += i != 0 ? "\n":"";
            debugJson += FormatColorStrings( jsonData[i]);
        }

        string filePath = Application.persistentDataPath + "/jsonData.txt";

        File.WriteAllLines(filePath, jsonData);
        Debug.Log("We have successfully saved Json data here... ");
        Debug.Log("<size=12><color=Green>" + filePath + " </color></size>");
        letestJsonData = debugJson;
    }


    string FormatColorStrings(string json)
    {
        string pattern = "\"#([0-9a-fA-F]{6})\"";
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
        System.Text.RegularExpressions.MatchCollection matches = regex.Matches(json);

        List<string> hexColorStrings = new List<string>();
        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            string hexColor = match.Groups[1].Value;
            string formattedColor = "<color=#" + hexColor + ">" + hexColor + "</color>";
            hexColorStrings.Add(formattedColor);
        }

        for (int i = 0; i < matches.Count; i++)
        {
            json = json.Replace(matches[i].Value, hexColorStrings[i]);
        }

        return json;
    }




    public List<string> GetAvailableColorsHex(List<Color> _availableColors)
    {
        List<string> hexColors = new List<string>();
        for (int i = 0; i < _availableColors.Count; i++)
        {
            string hex = ColorToHex(_availableColors[i]);
            hexColors.Add(hex);
        }
        return hexColors;
    }

    Color HexToColor(string hex)
    {
        hex = hex.TrimStart('#');

        if (hex.Length != 6)
        {
            Debug.LogWarning("Invalid hex color code format.");
            return Color.white;
        }

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, 255);
    }

    string ColorToHex(Color _color)
    {
        string hexColor = "#" + ColorUtility.ToHtmlStringRGB(_color);
        return hexColor;
    }


    [System.Serializable]
    struct SerializableShape
    {
        public string type;
        public List<string> availableColors;

        public SerializableShape(string _type, List<string> _availableColors)
        {
            type = _type;
            availableColors = _availableColors;
        }
    }
}

