using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce;
    Rigidbody playerRB;

    RaycastHit hitData;
    float hitCheckDistance = 0.1f;
    bool hitDetect1;
    bool hitDetect2;
    public LayerMask groundLayerMask;
    public Transform groundCheckOrigin1;
    public Transform groundCheckOrigin2;
    bool isAirborne;

    public bool enableControls = false;
    CameraController gameCamera;
    GameController gameController;

    Vector3 playerYeetDirection;
    public float yeetStrength;

    public Vector3 startPosition;

    public bool disableFarawayPlatforms = true;

    Animator animator;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gameCamera = FindObjectOfType<CameraController>();
        gameController = FindObjectOfType<GameController>();
        animator = GetComponentInChildren<Animator>();
        playerRB.freezeRotation = true;
        startPosition = transform.position;
    }

    void Update()
    {
        if (enableControls)
        {
            HandleControls();
        }
    }

    public bool isGrounded()
    {
        hitDetect1 = Physics.Raycast(groundCheckOrigin1.position, Vector3.down, out hitData, hitCheckDistance, groundLayerMask);
        hitDetect2 = Physics.Raycast(groundCheckOrigin2.position, Vector3.down, out hitData, hitCheckDistance, groundLayerMask);
        Debug.DrawRay(groundCheckOrigin1.position, Vector3.down * hitCheckDistance, Color.red);
        Debug.DrawRay(groundCheckOrigin2.position, Vector3.down * hitCheckDistance, Color.red);
        return hitData.collider != null;
    }

    void HandleControls()
    {
        if (Input.GetMouseButtonDown(0) == true && isGrounded())
        {
            playerRB.AddForce(Vector3.up * jumpForce);
            animator.Play("Jump");
        }


        if (!isGrounded())
        {
            if (!isAirborne)
            {
                isAirborne = !isAirborne;
            }
        }
        else
        {
            if (isAirborne)
            {
                animator.Play("Landing");
                LandOnNewPlatform();
                playerRB.angularVelocity = Vector3.zero;
                isAirborne = false;
            }
        }

    }

    void LandOnNewPlatform()
    {
        Platform platformBelow = hitData.collider.GetComponentInParent<Platform>();

        if (platformBelow != null)
        {
            if (!platformBelow.playerHasLanded)
            {
                gameCamera.activateCameraMove = true;
                gameController.AddScore();
                StopCoroutine(platformBelow.rotationCoroutine);
                platformBelow.playerHasLanded = true;
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            for (int i = 0; i < other.contacts.Length; i++)
            {
                if (Vector3.Angle(other.contacts[i].normal, Vector3.right) <= 5f || Vector3.Angle(other.contacts[i].normal, Vector3.left) <= 5f)
                {
                    Debug.Log("Collide");
                    playerYeetDirection = other.contacts[i].normal;
                    Die();
                }
            }
        }
        if (other.gameObject.layer == groundLayerMask && !isGrounded())
        {
            gameController.GameOver();
        }
    }

    public void Die()
    {
        playerRB.freezeRotation = false;
        enableControls = false;
        playerRB.AddForce(playerYeetDirection * yeetStrength);
        playerRB.angularVelocity *= yeetStrength;
        gameController.GameOver();
    }

    public void ResetTransform()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        playerRB.freezeRotation = true;
    }

}
