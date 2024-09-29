using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveX, moveZ;
    public Transform CharacterModel;

    Vector3 direction = new Vector3();
    public CharacterController cc;
    public Animator playeranim;
    public float speed, walkBackSpeed, sprintSpeed;
    float oldwSpeed;
    bool walking = false, isSprinting = false;

    #region Head Bob
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;
    #endregion

    public GameObject inventory;
    public static bool isInventoryOpen = false;

    public Transform playerCamera;
    public Quaternion cameraOldRotation;

    // Audio variables for footsteps and running
    public AudioClip[] Sound;
    private AudioSource audioSource;

     
    private void Start()
    {
        oldwSpeed = speed;
        jointOriginalPos = joint.localPosition;

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        direction = transform.right * moveX + transform.forward * moveZ;
        cc.Move(direction * speed * Time.deltaTime);
        PlaySound();
        HeadBob();

        if (direction != Vector3.zero)
        {
            walking = true;

            //Play footstep or run sound based on movement state
            
            
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
            cc.Move(Vector3.forward * 0.00000000001f * Time.deltaTime);
        }

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

    private void PlaySound()
    {
        if (walking)
        {
            if (audioSource.clip != Sound[0])
            {
                audioSource.Stop();
                audioSource.clip = Sound[0];
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (isSprinting)
        {
            if (audioSource.clip != Sound[1])
            {
                audioSource.Stop();
                audioSource.clip = Sound[1];
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void HeadBob()
    {
        if (walking)
        {
            timer += Time.deltaTime * bobSpeed * 0.9f;
        }  
        if (isSprinting)
        {
            timer += Time.deltaTime * (bobSpeed + sprintSpeed)* 1.5f ;
        }          
        joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        
        if (!walking && !isSprinting)
        {
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }
}
