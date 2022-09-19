using System;
using UnityEngine;

/**
 * A hack to stop rocket from rotation when it's landed. Prevents it from keeling over on its own after the game is over.
 */
public class SafeLanding : MonoBehaviour
{
    private readonly string playerTag = "Player";
    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag(playerTag);
        Debug.Assert(player != null, "Can't find the player!");
        playerBody = player.GetComponent<Rigidbody>();
        Debug.Assert(playerBody != null, "Player has no physics!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag))
        {
            // TO DO: disable only if player got the rocket "straight enough"
            playerBody.useGravity = false;
            playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag))
        {
            playerBody.useGravity = true;
            playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
