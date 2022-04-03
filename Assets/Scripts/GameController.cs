using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public PlatformController platformController;
    UIController uiController;
    CameraController cameraController;

    public bool firstRound = true;

    int score = 0;

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
        player.enableControls = false;
        cameraController = FindObjectOfType<CameraController>();
    }

    public void StartGame()
    {
        player.disableFarawayPlatforms = true;
        platformController.gameObject.SetActive(true);
        platformController.StartSpawningPlatforms();
        player.enableControls = true;
    }

    public void GameOver()
    {
        firstRound = false;
        player.disableFarawayPlatforms = false;
        StopCoroutine(platformController.spawnPlatformsRoutine);
        foreach (GameObject platformGameObject in platformController.platformsPool.pooledObjects)
        {
            if (platformGameObject.activeSelf == true)
            {
                Platform platform = platformGameObject.GetComponent<Platform>();
                StopCoroutine(platform.rotationCoroutine);

            }
        }
        uiController.ShowGameOverScreen(score);
    }

    public void ResetScene()
    {
        foreach (GameObject platformGameObject in platformController.platformsPool.pooledObjects)
        {
            if (platformGameObject.activeSelf == true)
            {
                platformGameObject.SetActive(false);
            }

        }
        cameraController.transform.position = cameraController.startPosition;
        score = 0;
        player.ResetTransform();
    }

    public void AddScore()
    {
        score += 1;
        uiController.SetScore(score);
    }
}
