using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape", menuName = "GameData/Shape", order = 1)]

public class So_Shape : ScriptableObject
{
    [SerializeField] string type;
    [SerializeField] List<Color> availableColors;
    [SerializeField] ShapeController prefab;


    public ShapeController Get_ShapeController() => prefab;

    public string GetShapeType() => type;

    public List<Color> GetAvailableColors() => availableColors;

    public void SetAvailableColors(List<Color> _availableColors) => availableColors = _availableColors;
    
}


