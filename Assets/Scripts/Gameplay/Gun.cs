using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] AudioSource sourceShoot;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        sourceShoot.PlayOneShot(sourceShoot.clip);
        GameObject ball = BallPoolManager.Instance.GetPooledBall();
        ball.transform.position = firePoint.position;
        ball.transform.rotation = firePoint.rotation;
        ball.SetActive(true);
    }
}
