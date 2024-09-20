﻿using System.Collections;
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

    private void Start()
    {
        oldwSpeed = speed;
        jointOriginalPos = joint.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        direction = transform.right * moveX + transform.forward * moveZ;
        cc.Move(direction * speed * Time.deltaTime);

        HeadBob();

        if (direction != Vector3.zero)
        {
          

            walking = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
                speed = sprintSpeed;
                playeranim.ResetTrigger("walk");
                playeranim.SetTrigger("sprint");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSprinting = false;
                speed = oldwSpeed ;
                playeranim.ResetTrigger("sprint");
                playeranim.SetTrigger("walk");
            }
        }
        else{
            walking = false;
            isSprinting = false;
            speed = oldwSpeed ;
            cc.Move(Vector3.forward * 0.00000000001f * Time.deltaTime);
        }
    }
  

   private void HeadBob()
    {
        if(walking)
        {
            // Calculates HeadBob speed during sprint
            if(isSprinting)
            {
                timer += Time.deltaTime * (bobSpeed + sprintSpeed);
            }
            // Calculates HeadBob speed during crouched movement
            // else if (isCrouched)
            // {
            //     timer += Time.deltaTime * (bobSpeed * speedReduction);
            // }
            // Calculates HeadBob speed during walking
            else
            {
                timer += Time.deltaTime * bobSpeed * 0.9f;
            }
            // Applies HeadBob movement
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            // Resets when play stops moving
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }
}


