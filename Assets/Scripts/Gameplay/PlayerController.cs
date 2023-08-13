using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeedY = 5f;
    [SerializeField] float rotationSpeedX = 5f;

    float rotationX, rotationY;
    float inputX, inputY;
  
    void Update()
    {
        // Handle player movement
        inputX = Input.GetAxis("Horizontal");
        inputY= Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(inputX, 0f, inputY);
        moveDirection = transform.TransformDirection(moveDirection).normalized;
        moveDirection.y = 0;
        transform.position += (moveDirection * moveSpeed * Time.deltaTime);

        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        rotationX -= inputY * rotationSpeedX * Time.deltaTime;
        rotationY += inputX * rotationSpeedY * Time.deltaTime;

        
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); // Adjust the range as needed

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}
