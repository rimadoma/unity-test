using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelaySeconds = 3.0f;
    private int nextLevelIndex;

    private void OnCollisionEnter(Collision collision)
    {
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
        GetComponent<RocketMovement>().enabled = false;

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

        Invoke(nameof(LoadNextLevel), levelLoadDelaySeconds);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }
}
