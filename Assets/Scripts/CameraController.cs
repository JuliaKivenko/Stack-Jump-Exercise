using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController player;
    Vector3 offset = new Vector3(0, 2.21f, -15.41f);

    public float cameraMoveSpeed;

    public bool activateCameraMove = false;

    public Vector3 startPosition;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        //for moving the camera when player jumps on a new platform
        if (activateCameraMove)
        {
            Vector3 endPosition = player.transform.position + offset;
            StartCoroutine(MoveCamera(cameraMoveSpeed, endPosition));
            activateCameraMove = false;
        }
    }

    //general coroutine for moving the camera
    public IEnumerator MoveCamera(float time, Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
