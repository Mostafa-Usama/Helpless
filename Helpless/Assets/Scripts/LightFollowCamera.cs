using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowCamera : MonoBehaviour
{
    public Transform PlayerCamera;
    public float rotationSpeed = 3f;
    
    // public Light FlashLight;
    // bool LightState = false;
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - PlayerCamera.position;
       

    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = PlayerCamera.position + offset;
        //transform.rotation = PlayerCamera.rotation * rotationOffset;
         Quaternion targetRotation = PlayerCamera.rotation;

       
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime
        );
       
    }
}
