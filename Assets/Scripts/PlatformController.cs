using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public ObjectPool platformsPool;

    public Vector2 minMaxSpawnInterval;
    float spawnInterval;

    public bool spawnPlatform = false;
    Vector3 spawnPosition;
    float heightOffset;

    public Coroutine spawnPlatformsRoutine;

    void Update()
    {
        if (spawnPlatform)
        {
            spawnPlatformsRoutine = StartCoroutine(SpawnPlatforms());
        }
    }

    public void StartSpawningPlatforms()
    {
        BoxCollider platformCollider = platformsPool.GetPooledObject().GetComponentInChildren<BoxCollider>();
        heightOffset = platformCollider.size.y;
        float platformHalfHeight = platformCollider.size.y / 2f;
        spawnPosition = new Vector3(transform.position.x, platformHalfHeight, transform.position.z);
        spawnPlatform = true;
    }

    public IEnumerator SpawnPlatforms()
    {
        spawnPlatform = false;
        spawnInterval = Random.Range(minMaxSpawnInterval.x, minMaxSpawnInterval.y);
        GameObject platformGameObject = platformsPool.GetPooledObject();
        if (platformGameObject != null)
        {
            Platform platform = platformGameObject.GetComponent<Platform>();
            platform.SetPlatformParameters(spawnPosition);
            platform.gameObject.SetActive(true);
            platform.rotationCoroutine = StartCoroutine(platform.Rotate());

        }
        spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + heightOffset, spawnPosition.z);
        yield return new WaitForSeconds(spawnInterval);

        spawnPlatform = true;
    }
}
