using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] So_ShapeManager shapeManager;      //Scriptable objet with all the shapes data and refrences
    [SerializeField] Transform playerTransform;
    [SerializeField] float minSpawnDistance;
    [SerializeField] float maxSpawnDistance;


   

    private void Awake()
    {
        InitializeData();
        SpawnShape();
    }

    /// <summary>
    /// Initialize all the data 
    /// </summary>
    void InitializeData()
    {
        Application.targetFrameRate = 60;
        shapeManager.RefreshData();
    }

    /// <summary>
    /// Spawn a random shape from avaialbe shapes
    /// </summary>

    [ContextMenu("Test Spawn")]
    void SpawnShape()
    {       
        //Geting random Shape for availbe shapes
        So_Shape randomShape =  shapeManager.GetRandomShape();

        //Genrating Random location and rotation
        Vector2 randomPoint = Random.insideUnitCircle.normalized;       
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomPoint.x, 0f, randomPoint.y) * randomDistance;
        spawnPosition.y = 0.5f;       
        Vector3 lookAtPosition = playerTransform.position - spawnPosition;
        lookAtPosition.y = 0; //spawnPosition.y; 
        Quaternion rotation = Quaternion.LookRotation(lookAtPosition, Vector3.up);

        //Spawing Shape with location and rotation and distance
        ShapeController spawnedShape = Instantiate(randomShape.Get_ShapeController(), spawnPosition, rotation);
        //applying random avaible color  onto the objects
        spawnedShape.SetShape(randomShape.GetAvailableColors());
        //Register event when this shape destroy we spawn another one
        spawnedShape.Register_OnShapeDamaged(Callback_OnShapeDamaged);
        Debug.Log("Spawned new Shape :" + randomShape.GetShapeType());
    }


    /// <summary>
    /// Called when the spaned shape object is destroyed
    /// </summary>
    void Callback_OnShapeDamaged()
    {
        SpawnShape();
    }

}
