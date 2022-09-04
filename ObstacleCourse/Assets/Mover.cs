using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Too high moveSpeed leads to player going through walls?
    [SerializeField] float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translation = getTranslation();
        transform.Translate(translation);
    }

    private Vector3 getTranslation()
    {
        Vector3 input = readInput();

        // Speed up with moveSpeed (dT is small)
        input = input * moveSpeed;

        // Make movement speed independent of FPS
        return input * Time.deltaTime;
    }

    private Vector3 readInput()
    {
        return new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            z = Input.GetAxis("Vertical")
        };
    }
}
