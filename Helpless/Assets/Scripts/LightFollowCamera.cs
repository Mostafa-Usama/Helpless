using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowCamera : MonoBehaviour
{
    public Transform PlayerCamera;
    public float speed = 3f;
    Vector3 offset = Vector3.zero;
    public Light FlashLight;
    bool LightState = false;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - PlayerCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerCamera.position - offset;
        FlashLight.transform.rotation = Quaternion.Slerp(FlashLight.transform.rotation, PlayerCamera.rotation, speed* Time.deltaTime );
        if (Input.GetKeyDown(KeyCode.E)){
           FlashLight.enabled = !FlashLight.enabled;
           LightState = !LightState;
        }
    }
}
