﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof( ShapeColorAssigner))]
public class ShapeController : MonoBehaviour
{
    ShapeColorAssigner shapeColorAssigner;

    [Header("Current Progress")]
    [SerializeField] bool isSelfDestructionActivated;

     Action OnShapeDameged;
    public  void Register_OnShapeDamaged(Action _callback) => OnShapeDameged = _callback;

    private void Awake()
    {
        shapeColorAssigner = GetComponent<ShapeColorAssigner>();
    }

    public void SetShape(List<Color> availableColors)
    {
        shapeColorAssigner.SetShapesColors(availableColors);
    }


    public void ActivateSelfDestruct()
    {
        if (isSelfDestructionActivated)
            return;
        Debug.Log("this shape is damaged and self destruct activate in 5 seconds");
        isSelfDestructionActivated = true;
        DisableKinatic();
        Invoke("DestroyShape", 10);
    }
    void DestroyShape()
    {
        OnShapeDameged?.Invoke();
        Destroy(gameObject);
    }

    void DisableKinatic()
    {       
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].useGravity = true;
            rbs[i].isKinematic = false;
        }
    }
}
