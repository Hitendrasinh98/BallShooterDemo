using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPoolManager : MonoBehaviour
{
    public static BallPoolManager Instance; // Singleton instance

    [SerializeField] GameObject ballPrefab;
    [SerializeField]int initialPoolSize = 10;

    private Queue<GameObject> ballPool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject ball = Instantiate(ballPrefab,transform);
            ball.SetActive(false);
            ballPool.Enqueue(ball);
        }
    }

    public GameObject GetPooledBall()
    {
        if (ballPool.Count > 0)
        {
            GameObject ball = ballPool.Dequeue();           
            return ball;
        }
        // If the pool is empty, create a new ball
        GameObject newBall = Instantiate(ballPrefab,transform);
        newBall.SetActive(false);
        return newBall;
    }

    public void ReturnToPool(GameObject ball)
    {
        ball.SetActive(false);
        ballPool.Enqueue(ball);
    }
}
