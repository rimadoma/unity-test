using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float rotationDegreesPerSecond = 180.0f;
    [SerializeField] float thrustPerSecond = 1000.0f;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        ApplyRotation();
        ApplyThrust();
    }

    private void ApplyThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * NormaliseToFPS(thrustPerSecond));
        }
    }

    private float NormaliseToFPS(float speedPerSecond)
    {
        return speedPerSecond * Time.deltaTime;
    }

    private void ApplyRotation()
    {
        var xAngle = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xAngle -= rotationDegreesPerSecond;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            xAngle += rotationDegreesPerSecond;
        }

        // Freeze rotation so that we can override the physics system
        rigidBody.freezeRotation = true;
        transform.Rotate(NormaliseToFPS(xAngle), 0.0f, 0.0f);
        rigidBody.freezeRotation = false;
    }
}
