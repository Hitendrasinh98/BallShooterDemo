using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( ShapeColorAssigner))]
public class ShapeController : MonoBehaviour
{
    ShapeColorAssigner shapeColorAssigner;
    private void Awake()
    {
        shapeColorAssigner = GetComponent<ShapeColorAssigner>();
    }

    public void SetShape(List<Color> availableColors)
    {
        shapeColorAssigner.SetShapesColors(availableColors);
    }
}
