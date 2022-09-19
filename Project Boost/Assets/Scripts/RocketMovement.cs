using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float rotationDegreesPerSecond = 180.0f;
    [SerializeField] float thrustPerSecond = 1000.0f;
    private Rigidbody rigidBody;
    private AudioSource thrustSFX;
    private readonly KeyCode thrustKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Debug.Assert(rigidBody != null, "Rocket has no physics!");
        thrustSFX = GetComponent<AudioSource>();
        Debug.Assert(thrustSFX != null, "Rocket has thrust audio missing!");
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
            thrustSFX.Stop();
            thrustSFX.mute = false;
            thrustSFX.loop = true;
            thrustSFX.Play();
        }
        else if (Input.GetKeyUp(thrustKey))
        {
            // Muting the sound instead of stopping avoids "popping"
            thrustSFX.mute = true;
            thrustSFX.loop = false;
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
        thrustSFX.Stop();
    }
}
