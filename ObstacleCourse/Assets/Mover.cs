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
        // TODO
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 translation = ReadDirectionKeys();

        // Speed up with moveSpeed (dT is small)
        translation *= moveSpeed;

        // Make movement speed independent of FPS
        translation *= Time.deltaTime;

        transform.Translate(translation);
    }
    private Vector3 ReadDirectionKeys()
    {
        return new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            z = Input.GetAxis("Vertical")
        };
    }
}
