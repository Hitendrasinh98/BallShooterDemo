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




    void OnCollisionEnter(Collision collision)
    {
        //instantlly we will distable ball
        DisableBall();
    }

    void DisableBall()
    {
        CancelInvoke("DisableBall");
        BallPoolManager.Instance.ReturnToPool(gameObject);
    }
}
