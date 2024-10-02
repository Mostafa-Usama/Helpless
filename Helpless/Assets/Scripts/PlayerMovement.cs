using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveX, moveZ;
    public Transform CharacterModel;

    Vector3 direction = new Vector3();
    public CharacterController cc;
    public Animator playeranim;
    public float speed, walkBackSpeed, sprintSpeed;
    public float gravity = -9.81f;    // Gravity value
    public float jumpHeight = 1.5f;   // Jump height (optional, if needed)
    private Vector3 velocity;         // Stores gravity-affected velocity
    private bool isGrounded;          // To check if the player is grounded

    // CharacterController-specific properties
    public float slopeLimit = 45f;    // Maximum slope the player can walk on
    public float stepOffset = 0.5f;   // Step height the player can walk over
    public float groundCheckDistance = 0.4f; // Distance to check for ground
    public LayerMask groundMask;      // Layer to define what counts as "ground"

    private float oldwSpeed;
    private bool walking = false, isSprinting = false;

    #region Head Bob
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    private Vector3 jointOriginalPos;
    private float timer = 0;
    #endregion

    public GameObject inventory;
    public static bool isInventoryOpen = false;

    public Transform playerCamera;
    public Quaternion cameraOldRotation;

    public AudioClip[] Sound;  // Array for sounds: 0 - walk, 1 - sprint
    private AudioSource audioSource;

    void Start()
    {
        oldwSpeed = speed;
        jointOriginalPos = joint.localPosition;
        audioSource = GetComponent<AudioSource>();

        // Set CharacterController properties for handling stairs
        cc.slopeLimit = slopeLimit;
        cc.stepOffset = stepOffset;
    }

    void Update()
    {
        GroundCheck();
        HandleMovement();
        PlaySound();
        HeadBob();
        HandleInventory();
    }

    // Checks if the player is grounded to handle gravity
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        // Reset velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Slight downward force to keep grounded
        }
    }

    // Handles movement and stair climbing
    private void HandleMovement()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        direction = transform.right * moveX + transform.forward * moveZ;

        if (direction != Vector3.zero)
        {
            cc.Move(direction * speed * Time.deltaTime);

            walking = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                walking = false;
                isSprinting = true;
                speed = sprintSpeed;
                playeranim.ResetTrigger("walk");
                playeranim.SetTrigger("sprint");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSprinting = false;
                walking = true;
                speed = oldwSpeed;
                playeranim.ResetTrigger("sprint");
                playeranim.SetTrigger("walk");
            }
        }
        else
        {
            walking = false;
            isSprinting = false;
            speed = oldwSpeed;
        }

        // Apply gravity to ensure smooth stair descent
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    // Play sound based on walking or sprinting
    private void PlaySound()
    {
        if (walking && !isSprinting)
        {
            if (audioSource.clip != Sound[0])
            {
                audioSource.Stop();
                audioSource.clip = Sound[0];  // Walking sound
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();  // Start playing if not already playing
            }
        }
        else if (isSprinting)
        {
            if (audioSource.clip != Sound[1])
            {
                audioSource.Stop();
                audioSource.clip = Sound[1];  // Sprinting sound
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();  // Start playing if not already playing
            }
        }
        else
        {
            audioSource.Stop();  // Stop when idle
        }
    }

    // Head bobbing effect for movement immersion
    private void HeadBob()
    {
        if (walking)
        {
            timer += Time.deltaTime * bobSpeed * 0.9f;
        }
        if (isSprinting)
        {
            timer += Time.deltaTime * (bobSpeed + sprintSpeed) * 1.5f;
        }

        joint.localPosition = new Vector3(
            jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x,
            jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y,
            jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z
        );

        if (!walking && !isSprinting)
        {
            timer = 0;
            joint.localPosition = new Vector3(
                Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed),
                Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed),
                Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed)
            );
        }
    }

    // Handles the inventory opening and closing
    private void HandleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeSelf);
            isInventoryOpen = !isInventoryOpen;
            cameraOldRotation = playerCamera.localRotation;

            if (isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                playerCamera.GetComponent<CameraMovement>().enabled = false;
            }
            else
            {
                playerCamera.localRotation = cameraOldRotation;
                Cursor.lockState = CursorLockMode.Locked;
                playerCamera.GetComponent<CameraMovement>().enabled = true;
            }
        }
    }
}
