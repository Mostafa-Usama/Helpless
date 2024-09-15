using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  
    // Start is called before the first frame update
    float moveX, moveZ;
    
    Vector3 direction = new Vector3();
    public CharacterController cc;
    public float moveSpeed = 10f;
    void Start()
    {
        
        
    }
  

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        direction = transform.right * moveX + transform.forward * moveZ;
        cc.Move(direction * moveSpeed * Time.deltaTime);
       
    }
  
}
