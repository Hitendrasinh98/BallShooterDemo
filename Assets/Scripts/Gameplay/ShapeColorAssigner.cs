using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeColorAssigner : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> mShapeElements;

    public void SetShapesColors(List<Color> availableColors)
    {

        for (int i = 0; i < mShapeElements.Count; i++)
        {
            Color randomColor = availableColors[Random.Range(0, availableColors.Count)];
            mShapeElements[i].material.SetColor("_BaseColor", randomColor); 
        }
    }    
}
