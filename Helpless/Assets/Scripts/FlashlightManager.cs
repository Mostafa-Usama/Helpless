using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightManager : MonoBehaviour
{
    public Light WhiteLight;
    public Slider BatteryLifeSlider;
    public Gradient BatteryLifeColor;
    public float BatteryLife;
    public float BatteryDrainRate;
    bool isFlashlightOn = false;
    public Image FillColor;
    AudioSource on_off_flash;

    public float amount;
    public float maxAmount;
    public float smoothAmount;
     private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        BatteryLife = BatteryLifeSlider.maxValue;
        initialPosition = transform.localPosition;
        on_off_flash = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);



         if (Input.GetKeyDown(KeyCode.F)){
            if (BatteryLife <= 0){
                // show message that battery is empty, play sound
            }
            else{
                WhiteLight.enabled = !WhiteLight.enabled;
                isFlashlightOn = !isFlashlightOn;
                on_off_flash.Play();
            }
        }
        if (isFlashlightOn && BatteryLife > 0){
            BatteryLife -= BatteryDrainRate * Time.deltaTime;
        }
        if (BatteryLife <= 0){
            
            isFlashlightOn = false;
            WhiteLight.enabled = false;
        }

        BatteryLifeSlider.value = BatteryLife;
        FillColor.color = BatteryLifeColor.Evaluate(BatteryLifeSlider.normalizedValue);
        BatteryLife = Mathf.Clamp(BatteryLife, 0f, 100f);
    }
}
