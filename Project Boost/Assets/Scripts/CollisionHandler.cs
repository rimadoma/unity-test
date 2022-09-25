using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelaySeconds = 3.0f;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip finishSFX;
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
        PlayLevelEndAudio(success);
        IncrementNextLevelIndex(success);
        Invoke(nameof(LoadNextLevel), levelLoadDelaySeconds);
    }

    private void PlayLevelEndAudio(bool success)
    {
        AudioClip audio = success ? finishSFX : explosionSFX;
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
