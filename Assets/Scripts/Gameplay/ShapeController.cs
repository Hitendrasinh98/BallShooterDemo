using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof( ShapeColorAssigner))]
public class ShapeController : MonoBehaviour
{
    [SerializeField] AudioSource sourceShoot;


    [Header("Current Progress")]
    [SerializeField] bool isSelfDestructionActivated;       //keep trac of is this damaged or not

    ShapeColorAssigner shapeColorAssigner;  //helper class to set random colors
    Action OnShapeDameged;              //Action to notify subscirber about the damage status
    public  void Register_OnShapeDamaged(Action _callback) => OnShapeDameged = _callback;

    private void Awake()
    {
        shapeColorAssigner = GetComponent<ShapeColorAssigner>();
    }


    /// <summary>
    /// initialing data as soon as shape is activated
    /// </summary>
    /// <param name="availableColors"></param>
    public void SetShape(List<Color> availableColors)
    {
        isSelfDestructionActivated = false;
        shapeColorAssigner.SetShapesColors(availableColors);
    }

    /// <summary>
    /// Start self destruct which take certain seconds to destroy completelly
    /// </summary>
    public void ActivateSelfDestruct()
    {
        sourceShoot.PlayOneShot(sourceShoot.clip);
        if (isSelfDestructionActivated)
            return;

        Debug.Log("this shape is damaged and self destruct activate in 7 seconds");
        isSelfDestructionActivated = true;      
        Invoke("DestroyShape", 7);
    }

    /// <summary>
    /// Destroy gameobject and fire event to notify all
    /// </summary>
    void DestroyShape()
    {
        OnShapeDameged?.Invoke();
        Destroy(gameObject);
    }

    
}
