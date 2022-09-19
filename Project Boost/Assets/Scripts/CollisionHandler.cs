using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelaySeconds = 3.0f;
    [SerializeField] AudioClip explosionSFX;
    RocketMovement rocketMovement;
    private AudioSource audioSource;
    private int nextLevelIndex;
    private bool gameOver;

    private void Start()
    {
        gameOver = false;
        rocketMovement = GetComponent<RocketMovement>();
        audioSource = rocketMovement.GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(explosionSFX);
        }

        gameOver = true;

        Invoke(nameof(LoadNextLevel), levelLoadDelaySeconds);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }
}
