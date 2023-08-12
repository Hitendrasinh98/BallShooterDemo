using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/ShapeManager",order =0)]
public class So_ShapeManager : ScriptableObject
{
    [SerializeField] TextAsset jsonFile;
    [SerializeField] List<So_Shape> mShapes;
    
    [HideInInspector]public string letestJsonData; //for Custom Editor to see the jsonData 

    #region Shapes Featres

    public void RefreshData()
    {
        Debug.Log("Loading JsonData");
        string[] lines =  jsonFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length < 5)
                continue;

            //loading json Data
            SerializableShape shapeData = JsonUtility.FromJson<SerializableShape>(lines[i]);
            for (int j = 0; j < mShapes.Count; j++)
            { 
                //matching shape type in our AvailableShapes list
                if(shapeData.type == mShapes[j].GetShapeType())
                {
                    //Converting hex into colors
                    List<Color> availableColors = ConvertHexsIntoColors(shapeData.availableColors);
            
                    //Saving data back to scriptable object
                    mShapes[j].SetAvailableColors(availableColors);
                    break;
                }

                if(j== mShapes.Count -1)                
                    Debug.LogError("We didnt find the match for this Shape :" + shapeData.type);
                
            }
            
        }
    }


    public So_Shape GetRandomShape()
    {
        int index = Random.Range(0, mShapes.Count);

        return mShapes[index];
    }
    #endregion


    #region JsonData Funtionality

    /// <summary>
    /// Will convert SO shapes data into json
    /// and custom editor tool will display with nice formate on inspecotor
    /// need to copy that text and paste into our json.txt file if we want to implement that data
    /// </summary>
    public void SaveIntoJson()
    {
        //Creating serializatble json data from Shapes Data
        List<SerializableShape> serializableShapes = new List<SerializableShape>();
        foreach (So_Shape shape in mShapes)
        {
            serializableShapes.Add(new SerializableShape(shape.GetShapeType() , ConvertColorsIntoHexs( shape.GetAvailableColors())));
        }
        //Converting serializable data into jsonString
        string[] jsonData = new string[serializableShapes.Count];
        string debugJson = "";
        for (int i = 0; i < serializableShapes.Count; i++)
        {
            jsonData[i] =  JsonUtility.ToJson(serializableShapes[i]);
            debugJson += i != 0 ? "\n":"";
            debugJson += FormatColorStrings( jsonData[i]);
        }

        //Caching the jsonData for custom editor tool
        letestJsonData = debugJson;
    }

    /// <summary>
    /// this is helper funtion to add the formate of text in json text 
    /// NOTE : This can only be used for debug cause it will add some tags into json text
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    string FormatColorStrings(string json)
    {
        string pattern = "\"#([0-9a-fA-F]{6})\"";
        System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(json, pattern);

        List<string> hexColorStrings = new List<string>();
        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            string hexColor = "#" + match.Groups[1].Value;
            string formattedColor = "<color=" + hexColor + ">\"" + hexColor + "\"</color>";
            hexColorStrings.Add(formattedColor);
        }

        for (int i = 0; i < matches.Count; i++)
        {
            json = json.Replace(matches[i].Value, hexColorStrings[i]);
        }

        return json;
    }



    /// <summary>
    /// used to convert the color into hex code so that we can easyly see in inspecor which colors are used
    /// </summary>
    /// <param name="_availableColors"></param>
    /// <returns></returns>
    public List<string> ConvertColorsIntoHexs(List<Color> _availableColors)
    {
        List<string> hexColors = new List<string>();
        for (int i = 0; i < _availableColors.Count; i++)
        {
            string hex = ColorToHex(_availableColors[i]);
            hexColors.Add(hex);
        }
        return hexColors;
    }

    public List<Color> ConvertHexsIntoColors(List<string> _availableColorsHex)
    {
        List<Color> convertedColors= new List<Color>();
        for (int i = 0; i < _availableColorsHex.Count; i++)
        {
            Color color= HexToColor(_availableColorsHex[i]);
            convertedColors.Add(color);
        }
        return convertedColors;
    }

    /// <summary>
    /// Convert back to color from hex code
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Convert color into hex code to save into json
    /// </summary>
    /// <param name="_color"></param>
    /// <returns></returns>
    string ColorToHex(Color _color)
    {
        string hexColor = "#" + ColorUtility.ToHtmlStringRGB(_color);
        return hexColor;
    }


    #endregion



    /// <summary>
    /// Special  struct to store data into the json
    /// </summary>
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

