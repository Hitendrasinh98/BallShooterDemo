using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controll the movement and collision of balls that are shooted from gun
/// </summary>
public class Ball : MonoBehaviour
{
    [SerializeField]Rigidbody rb;
    [SerializeField] int shootForce = 10;

    private void OnEnable()
    {        
        if(rb == null)
        {
            DisableBall();
            return;
        }

        //auto distruction incase ball didnt collide with anthing
        Invoke("DisableBall", 3);
        //Reseting all the ovemtn if this ball has
        rb.velocity = Vector3.zero;
        rb.angularVelocity= Vector3.zero;
        //applying forceinto forward direction
        rb.velocity = transform.forward * shootForce;
    }



    [SerializeField] int collisionForce = 110;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("ShapeElement"))
        {
            Debug.Log("We hit Shape");
            //adding the extra force to the collided objects
            collision.rigidbody.AddForce(transform.forward.normalized * collisionForce, ForceMode.Impulse);

            //activating self destruction to that shape
            ShapeController shapeController = collision.collider.GetComponentInParent<ShapeController>();
            if (shapeController)
                shapeController.ActivateSelfDestruct();
        }

        //disablng this ball back to the pool
        DisableBall();
    }

    void DisableBall()
    {
        CancelInvoke("DisableBall");
        BallPoolManager.Instance.ReturnToPool(gameObject);
    }
}
