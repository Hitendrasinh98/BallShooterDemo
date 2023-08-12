using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] So_ShapeManager shapeManager;
    [SerializeField] Transform playerTransform;

    private void Awake()
    {
        InitializeData();
    }


    void InitializeData()
    {
        shapeManager.RefreshData();
    }

    void SpawnShape()
    {
        //Get the random shape prefab and colors
        //Spawn on random location with random distance
    }

}
