using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float zSpeed;
    
    void FixedUpdate()
    {
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }
}
