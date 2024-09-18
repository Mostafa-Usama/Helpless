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
    public float wSpeed, wbSpeed, rnSpeed;
    float oldwSpeed;
    bool walking = false;
    
    private void Start()
    {
        oldwSpeed = wSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        direction = transform.right * moveX + transform.forward * moveZ;
        cc.Move(direction * wSpeed * Time.deltaTime);


        
        if (Input.GetKeyDown(KeyCode.W))
        {
            playeranim.SetTrigger("walk");
            playeranim.ResetTrigger("idle");
            walking = true;
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            playeranim.ResetTrigger("walk");
            playeranim.SetTrigger("idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            playeranim.SetTrigger("walkback");
            playeranim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playeranim.ResetTrigger("walkback");
            playeranim.SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {   
            playeranim.SetTrigger("walk");
            playeranim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playeranim.ResetTrigger("walk");
            playeranim.SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playeranim.SetTrigger("walk");
            playeranim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playeranim.ResetTrigger("walk");
            playeranim.SetTrigger("idle");
        }
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                wSpeed = wSpeed + rnSpeed;
                playeranim.ResetTrigger("walk");
                playeranim.SetTrigger("sprint");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                wSpeed = oldwSpeed ;
                playeranim.ResetTrigger("sprint");
                playeranim.SetTrigger("walk");
            }
        }
    }
  
}
