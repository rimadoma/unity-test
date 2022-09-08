using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    Boolean bumped;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!bumped && collision.collider.CompareTag("Player"))
        {
            changeColour();
            bumped = true;
        }
    }

    private void changeColour()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
