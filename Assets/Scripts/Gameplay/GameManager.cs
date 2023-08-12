using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] So_ShapeManager shapeManager;
    [SerializeField] Transform playerTransform;
    [SerializeField] float minSpawnDistance;
    [SerializeField] float maxSpawnDistance;

    [Header("Current Progress")]
    [SerializeField] GameObject go_TargetShape;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        InitializeData();
        SpawnShape();
    }


    void InitializeData()
    {
        shapeManager.RefreshData();
    }

    [ContextMenu("Test Spawn")]
    void SpawnShape()
    {
        if (go_TargetShape)
            Destroy(go_TargetShape);

        //Get the random shape prefab and colors
        //Spawn on random location with random distance
        So_Shape randomShape =  shapeManager.GetRandomShape();

        Vector2 randomPoint = Random.insideUnitCircle.normalized;

        // Map the random point to the desired spawn distance range
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomPoint.x, 0f, randomPoint.y) * randomDistance;
        spawnPosition.y = 0;
        // Calculate the rotation to look at the player without changing the Y-axis rotation
        Vector3 lookAtPosition = playerTransform.position - spawnPosition;
        lookAtPosition.y = 0; //spawnPosition.y; 
        Quaternion rotation = Quaternion.LookRotation(lookAtPosition, Vector3.up);

        go_TargetShape = Instantiate(randomShape.Get_ShapeController(),spawnPosition,rotation).gameObject;
    }

}
