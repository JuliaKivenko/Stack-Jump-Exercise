using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlatformController platformController;
    public Vector2 minMaxRotSpeed;
    float rotSpeed;
    public bool shouldStopMoving = false;
    public bool playerHasLanded = false;

    public Coroutine rotationCoroutine;

    void Start()
    {
        platformController = FindObjectOfType<PlatformController>();
    }

    //set random speed and starting position of the platform
    public void SetPlatformParameters(Vector3 startPosition)
    {
        shouldStopMoving = false;
        playerHasLanded = false;
        transform.position = startPosition;
        rotSpeed = Random.Range(minMaxRotSpeed.x, minMaxRotSpeed.y);
        int sideToSpawn = Random.Range(0, 2);
        switch (sideToSpawn)
        {
            case 0:
                transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 1:
                transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }

    //rotate the platform from starting position to 0
    public IEnumerator Rotate()
    {
        float timeElapsed = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        while (timeElapsed < rotSpeed)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / rotSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
