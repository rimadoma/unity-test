using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float rotationDegreesPerSecond = 180.0f;
    [SerializeField] float thrustPerSecond = 1000.0f;
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

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
        bool thrusting = ApplyThrust() | ApplySideThrust();

        if (thrusting)
        {
            PlayThrustAudio();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private bool ApplySideThrust()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(-rotationDegreesPerSecond);
            PlayThrustParticles(rightThrustParticles);
            return true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(rotationDegreesPerSecond);
            PlayThrustParticles(leftThrustParticles);
            return true;
        }
        else
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
            return false;
        }
    }

    private bool ApplyThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * NormaliseToFPS(thrustPerSecond));
            PlayThrustParticles(mainThrustParticles);
            return true;
        }
        else
        {
            mainThrustParticles.Stop();
            return false;
        }
    }

    private void Rotate(float angle)
    {
        // Freeze rotation so that we can override the physics system
        rigidBody.freezeRotation = true;
        transform.Rotate(NormaliseToFPS(angle), 0.0f, 0.0f);
        rigidBody.freezeRotation = false;
    }

    private float NormaliseToFPS(float speedPerSecond)
    {
        return speedPerSecond * Time.deltaTime;
    }

    private void PlayThrustAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSFX);
        }
    }

    private void PlayThrustParticles(ParticleSystem system)
    {
        if (!system.isPlaying)
        {
            system.Play();
        }
    }

    private void StopAllParticleSystems()
    {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (var system in particleSystems)
        {
            system.Stop();
        };
    }

    private void OnDisable()
    {
        audioSource.Stop();

        StopAllParticleSystems();
    }
}
