using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * A hack to stop rocket from rotation when it's landed. Prevents it from keeling over on its own after the game is over.
 */
public class SafefLanding : MonoBehaviour
{
    private string playerTag = "Player";
    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerBody = player.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag) && playerBody != null)
        {
            Debug.Log("Landed");
            // TO DO: disable only if player got the rocket "straight enough"
            playerBody.useGravity = false;
            playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag) && playerBody != null)
        {
            Debug.Log("Taking off");
            playerBody.useGravity = true;
            playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
