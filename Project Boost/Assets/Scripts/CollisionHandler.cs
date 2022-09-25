using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelaySeconds = 3.0f;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] ParticleSystem finishParticles;
    RocketMovement rocketMovement;
    private AudioSource audioSource;
    private int nextLevelIndex;
    private bool gameOver;

    private void Start()
    {
        gameOver = false;
        rocketMovement = GetComponent<RocketMovement>();
        Debug.Assert(rocketMovement != null, "Couldn't find Player script!");
        audioSource = rocketMovement.GetComponent<AudioSource>();
        Debug.Assert(audioSource != null, "No audio source!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameOver)
        {
            return;
        }

        switch(collision.collider.tag)
        {
            case "Finish":
                Debug.Log("Finish!");
                LevelEndSequence(true);
                break;
            case "Friendly":
                Debug.Log("Bump, no damage");
                break;
            default:
                Debug.Log("Bump, ouch");
                LevelEndSequence(false);
                break;
        }
    }

    private void LevelEndSequence(bool success)
    {
        rocketMovement.enabled = false;
        gameOver = true;
        PlayLevelEndFX(success);
        IncrementNextLevelIndex(success);
        Invoke(nameof(LoadNextLevel), levelLoadDelaySeconds);
    }

    private void PlayLevelEndFX(bool success)
    {
        ParticleSystem particles;
        AudioClip audio;
        if (success)
        {
            particles = finishParticles;
            audio = finishSFX;
        }
        else
        {
            particles = explosionParticles;
            audio = explosionSFX;
        }

        particles.Play();

        audioSource.mute = false;
        audioSource.PlayOneShot(audio);
    }

    private void IncrementNextLevelIndex(bool success)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (success)
        {
            nextLevelIndex = currentIndex + 1;
            if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("You is teh ultimate champion!");
                nextLevelIndex = 0;
            }
        }
        else
        {
            nextLevelIndex = currentIndex;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }
}
