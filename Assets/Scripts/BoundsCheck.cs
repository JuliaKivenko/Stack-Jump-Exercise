using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    GameObject parentObject;
    PlayerController player;
    private void Start()
    {
        parentObject = transform.parent.gameObject;
        player = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bounds" && player.disableFarawayPlatforms)
        {
            parentObject.SetActive(false);
        }
    }
}
