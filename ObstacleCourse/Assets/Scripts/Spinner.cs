using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spinner : MonoBehaviour
{
    [SerializeField] float degreesPerSecond = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float degreesPerFrame = degreesPerSecond * Time.deltaTime;
        transform.Rotate(0.0f, degreesPerFrame, 0.0f);
    }
}
