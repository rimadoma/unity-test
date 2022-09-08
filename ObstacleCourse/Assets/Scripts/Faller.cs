using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Faller : MonoBehaviour
{
    [SerializeField] float minimumDelay = 0.0f;
    [SerializeField] float maximumDelay = 10.0f;
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private float fallStartTime;
    private bool fallStarted;

    // Start is called before the first frame update
    void Start()
    {
        fallStartTime = Random.value * (maximumDelay - minimumDelay) + minimumDelay;
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        rigidBody.useGravity = false;
        meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fallStarted && fallStartTime <= Time.time)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<MeshRenderer>().enabled = true;
            fallStarted = true;
        }
    }
}
