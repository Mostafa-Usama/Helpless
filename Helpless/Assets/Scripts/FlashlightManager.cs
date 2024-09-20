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

    // Start is called before the first frame update
    void Start()
    {
        BatteryLife = BatteryLifeSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.F)){
            if (BatteryLife <= 0){
                // show message that battery is empty, play sound
            }
            else{
                WhiteLight.enabled = !WhiteLight.enabled;
                isFlashlightOn = !isFlashlightOn;
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
