using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float rotationDegreesPerSecond = 180.0f;
    [SerializeField] float thrustPerSecond = 1000.0f;
    [SerializeField] AudioClip thrustSFX;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private readonly KeyCode thrustKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Debug.Assert(rigidBody != null, "Rocket has no physics!");
        audioSource = GetComponent<AudioSource>();
        Debug.Assert(audioSource != null, "Rocket has no audio source!");
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
        ApplyThrustSound();
    }

    private void ApplyThrust()
    {
        if (Input.GetKey(thrustKey))
        {
            rigidBody.AddRelativeForce(Vector3.up * NormaliseToFPS(thrustPerSecond));
        }
    }

    private float NormaliseToFPS(float speedPerSecond)
    {
        return speedPerSecond * Time.deltaTime;
    }

    private void ApplyThrustSound()
    {
        if (Input.GetKeyDown(thrustKey))
        {
            // Stop the SFX so that the loop starts from the beginning
            audioSource.Stop();
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.PlayOneShot(thrustSFX);
        }
        else if (Input.GetKeyUp(thrustKey))
        {
            // Muting the sound instead of stopping avoids "popping"
            audioSource.mute = true;
            audioSource.loop = false;
        }
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

    private void OnDisable()
    {
        audioSource.Stop();
    }
}
