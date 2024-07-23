using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float friction;

    private float _yVel;
    private Vector2 _moveVel;
    private Vector3 _velocity;

    private float _xRotation;
    private float _yRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // Direction.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _yRotation += mouseX;
        // transform.Rotate(Vector3.up * mouseX);
        transform.rotation = Quaternion.Euler(new Vector3(_xRotation, _yRotation, 0f));
        
        // Movement.
        _moveVel.x += ((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0)) * movementSpeed * Time.deltaTime;
        _moveVel.y += ((Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0)) * movementSpeed * Time.deltaTime;
        
        _yVel += ((Input.GetKey(KeyCode.Space) ? 1 : 0) - (Input.GetKey(KeyCode.LeftShift) ? 1 : 0)) * movementSpeed * Time.deltaTime;
        
        _velocity = transform.right * _moveVel.x + transform.forward * _moveVel.y + Vector3.up * _yVel;
        transform.position += _velocity;
    }

    private void FixedUpdate()
    {
        _moveVel *= friction;
        _yVel *= friction;
    }

    private Vector3 setX(Vector3 vec3, float x)
    {
        vec3.x = x;
        return vec3;
    }
}
