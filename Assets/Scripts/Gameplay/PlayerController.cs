using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeedY = 5f;
    [SerializeField] float rotationSpeedX = 5f;

    float rotationX, rotationY;
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection).normalized;

        transform.position += (moveDirection * moveSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * rotationSpeedX * Time.deltaTime;
        rotationY += mouseX * rotationSpeedY * Time.deltaTime;

        
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); // Adjust the range as needed

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}
