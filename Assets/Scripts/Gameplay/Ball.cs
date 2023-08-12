using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        Invoke("DisableBall", 4);
        rb.velocity = Vector3.zero;
        rb.angularVelocity= Vector3.zero;

        rb.velocity = transform.forward * shootForce;
    }



    [SerializeField] int collisionForce = 110;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("ShapeElement"))
        {
            Debug.Log("We hit Shape");
            collision.rigidbody.AddForce(transform.forward.normalized * collisionForce, ForceMode.Impulse);

            ShapeController shapeController = collision.collider.GetComponentInParent<ShapeController>();
            if (shapeController)
                shapeController.ActivateSelfDestruct();
        }

        DisableBall();
    }

    void DisableBall()
    {
        CancelInvoke("DisableBall");
        BallPoolManager.Instance.ReturnToPool(gameObject);
    }
}
