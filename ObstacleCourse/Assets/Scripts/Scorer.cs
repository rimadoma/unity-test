using System.Collections;
using System.Collections.Generic;
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

        if (!collider.CompareTag(hitTag))
        {
            collider.tag = hitTag;
            bumps++;
            Debug.Log("Ow, I've hit " + bumps + " obstacles");
        }
    }
}
