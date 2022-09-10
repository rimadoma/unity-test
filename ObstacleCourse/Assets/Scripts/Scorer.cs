using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int bumps;
    private readonly string hitTag = "Hit";

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
        Collider collider = collision.collider;

        if (!collider.CompareTag(hitTag) && !collider.CompareTag("Finish") && !collider.CompareTag("Ground"))
        {
            collider.tag = hitTag;
            bumps++;
            Debug.Log("Ow, I've hit " + bumps + " obstacles");
        }
    }

    public void logFinalScore()
    {
        TimeSpan time = TimeSpan.FromSeconds(Time.time);
        string timeToComplete = time.ToString(@"hh\:mm\:ss");

        Debug.Log("Obstacles hit: " + bumps + ". Course took: " + timeToComplete + " to complete");
    }
}
