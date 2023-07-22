using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed, inertia,rotationSpeed;
    
    void Update()
    {
        float moveDirection = 0;

        if (Input.GetKey(KeyCode.W)) moveDirection = 1;
        else if (Input.GetKey(KeyCode.S)) moveDirection = -1;

        float rotationDirection = 0;

        if (Input.GetKey(KeyCode.D)) rotationDirection = -1;
        else if (Input.GetKey(KeyCode.A)) rotationDirection = 1;

        transform.Translate(Vector3.up * moveDirection * movementSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0,0,rotationSpeed*rotationDirection) * Time.deltaTime);
    }
}
